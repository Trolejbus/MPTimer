import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AgentRuntimeDto, AgentRuntimeService } from '@app/features';
import { WorkspaceEventDto, WorkspaceEventService } from '@app/features/workspace-events';
import { TimeSpan } from '@app/shared';
import { combineLatest, interval } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

const BREAK_PER_HOUR_IN_SECONDS = 375;
const HOURS_PER_DAY = 8;

@Component({
  selector: 'app-time-worked-widget',
  templateUrl: './time-worked-widget.component.html',
  styleUrls: ['./time-worked-widget.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TimeWorkedWidgetComponent implements OnInit {

  public data = {
    labels: ['',''],
    datasets: [
      {
        data: [3, 1],
        backgroundColor: [
          "#336B87",
          "#FFFFFF",
        ],
        hoverBackgroundColor: [
          "#204456",
          "#FFFFFF",
        ],
      },
    ],  
  };

  public chartOptions = {
    cutout: '80%',
    plugins: {
      legend: {
        display: false,
      },
      tooltip: {
        enabled: false,
      }
    },
  };

  public vm$ = combineLatest([
    this.agentRuntimeService.agentRuntimes$.pipe(startWith([])),
    this.workspaceEventService.workspaceEvents$.pipe(startWith([])),
    interval(1000).pipe(startWith(0)),
  ]).pipe(
    map(([runtimes, workspaceEvents]) => ({
      runtimes: runtimes.sort((a, b) => a.from > b.from ? 1 : (a.from < b.from ? -1 : 0)),
      workspaceEvents,
    })),
    map((vm) => ({
      ...vm,
      startedAt: this.getStartedAt(vm.runtimes),
      finishedAt: this.getFinishedAt(vm.runtimes),
      breakTime: this.getBreakTime(vm.runtimes, vm.workspaceEvents),
      currentTime: new Date(),
    })),
    map((vm) => ({
      ...vm,
      workTime: this.getWorkTime(vm.startedAt, vm.finishedAt, vm.breakTime),
      plannedFinishedAt: this.getPlannedFinishedAt(vm.startedAt, vm.breakTime),
    })),
    map((vm) => ({
      ...vm,
      chartData: this.getChartData(vm.workTime),
    })),
  );

  constructor(
    private agentRuntimeService: AgentRuntimeService,
    private workspaceEventService: WorkspaceEventService,
  ) { }

  ngOnInit(): void {
  }

  public formatTime(timeSpan: TimeSpan, showSeconds = true): string {
    let result = timeSpan.hour < 10 ? `0${timeSpan.hour}` : timeSpan.hour.toString();
    result += ':' + (timeSpan.minute < 10 ? `0${timeSpan.minute}` : timeSpan.minute.toString());
    return result += showSeconds ? ':' + (timeSpan.second < 10 ? `0${timeSpan.second}` : timeSpan.second.toString()) : '';
  }

  private getStartedAt(runtimes: AgentRuntimeDto[]): Date | null {
    return runtimes[0]?.from ?? null;
  }

  private getFinishedAt(runtimes: AgentRuntimeDto[]): Date | null {
    let previous: Date | null = null;
    for (const runtime of runtimes) {
      if (previous == null || runtime.to == null || runtime.to > previous) {
        previous = runtime.to ?? null;
      }
    }

    return previous;
  }

  private getBreakTime(runtimes: AgentRuntimeDto[], workspaceEvents: WorkspaceEventDto[]): TimeSpan {
    const runtimesBreaks = this.getRuntimeBreaks(runtimes);
    const breakTimeGaps = this.getUnified(runtimesBreaks, workspaceEvents.map(r => ({ from: r.from, to: r.to ?? new Date()})));
    const breakInSeconds = breakTimeGaps.reduce((p, c) => p + Math.floor((c.to.getTime() - c.from.getTime()) / 1000), 0); 
    return TimeSpan.createFromSeconds(breakInSeconds);
  }

  private getWorkTime(startedAt: Date | null, finishedAt: Date | null, breakTime: TimeSpan): TimeSpan {
    if (startedAt == null) {
      return TimeSpan.empty();
    }

    const fullSeconds = Math.floor(((finishedAt ?? new Date())?.getTime() - startedAt.getTime()) / 1000)
    const workingSeconds = fullSeconds - breakTime.totalSeconds();
    const payedSeconds = Math.floor(workingSeconds / (1 - BREAK_PER_HOUR_IN_SECONDS / 3600));
    return TimeSpan.createFromSeconds(payedSeconds);
  }

  private getPlannedFinishedAt(startedAt: Date | null, breakTime: TimeSpan): Date {
    const startedAtTime = (startedAt ?? new Date()).getTime() / 1000;
    const afterBreaks = startedAtTime + breakTime.totalSeconds();
    const afterPlannedWork = afterBreaks + HOURS_PER_DAY * (1 - BREAK_PER_HOUR_IN_SECONDS / 3600) * 3600;
    return new Date(afterPlannedWork * 1000);
  }

  private getRuntimeBreaks(runtimes: AgentRuntimeDto[]): TimeGap[] {
    const runtimesUnified = this.getUnified(runtimes.map(r => ({ from: r.from, to: r.to ?? new Date()})));
    return runtimesUnified.reduce<{ previous: TimeGap | null, result: TimeGap[] }>((p, c) => ({
        previous: c,
        result: (p.previous != null && c.from > p.previous.to) ?
          [...p.result, { from: p.previous.to, to: c.from }] :
          p.result,
      }), { previous: null, result: [] }).result;
  }

  private getUnified(...sources: TimeGap[][]): TimeGap[] {
    const unioned = sources.reduce((p, c) => [...p, ...c], []);
    const ordered = unioned.sort((a, b) => a.from > b.from ? 1 : (a.from < b.from ? -1 : 0));
    return ordered.reduce<TimeGap[]>((p, c) => {
      const last = p[p.length - 1];
      if (last == null || c.from > last.to) {
        return [...p, {
          to: c.to,
          from: c.from,
        }];
      }

      if (c.to > last.to) {
        last.to = c.to;
      }

      return p;
    }, []);
  }

  private getChartData(workTime: TimeSpan): any {
    const timeToWork = new TimeSpan(8, 0, 0).totalSeconds() - workTime.totalSeconds();
    this.data.datasets[0].data = [Math.max(0, timeToWork), workTime.totalSeconds()];
    return this.data;
  }
}

interface TimeGap {
  from: Date;
  to: Date;
}

import { powerMonitor } from 'electron';

export class WorkspaceEventService {
  public static init(): void {
    powerMonitor.addListener('lock-screen', () => {
      console.log('lock');
      // Screen is locked, do something
    });
  
    powerMonitor.addListener('unlock-screen', () => {
      console.log('unlock');
      // Screen is unlocked, do something else
    });
  }
}

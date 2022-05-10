import * as signalR from '@microsoft/signalr';

chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
    switch (request.type) {
      case 'GoogleMeet_Call_Started':
          sendResponse({farewell: "GoogleMeet_Call_Started " + request.value.meetingId + " " + request.value.meetingTitle});
          break;
      case 'GoogleMeet_Call_End':
          sendResponse({farewell: "GoogleMeet_Call_End " + request.value.meetingId});
          break;
    }
  }
);

class SignalRService {
  private connection: signalR.HubConnection;
  private state: "NotConnected" | "Connecting" | "Connected" = "NotConnected"

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7109/Agent?agentId=b9a5448c-d41f-40ba-a0d4-ebe83683f9a6&type=chromeWidget")
      .build();
    this.connection.onclose(() => { this.state = "NotConnected" });
    setInterval(() => {
      this.clockTick();
    }, 5000);
  }

  async connect() {
    this.state = 'Connecting';
    try {
      await this.connection.start();
      this.state = 'Connected';

      console.log("SignalR Connected.");
    } catch (err) {
      console.log(err);
      this.state = 'NotConnected';
    }
  }

  async clockTick() {
    if (this.state !== 'NotConnected') {
      return;
    }

    await this.connect();
  }
}

const singalR = new SignalRService();
singalR.connect();

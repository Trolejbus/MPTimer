import * as signalR from '@microsoft/signalr';

const url = "https://mptimer.azurewebsites.net";
const agentId = "5e6605e9-713b-4a9c-a720-7c42745649f4"; // b9a5448c-d41f-40ba-a0d4-ebe83683f9a6"; // prod: b9a5448c-d41f-40ba-a0d4-ebe83683f9a6
let currentMeeting = generateGuid();
let model: any = null;
chrome.runtime.onMessage.addListener(
  function(request, sender, sendResponse) {
    switch (request.type) {
      case 'GoogleMeet_Call_Started':
          sendResponse({farewell: "GoogleMeet_Call_Started2 " + request.value.meetingId + " " + request.value.meetingTitle});
          currentMeeting = generateGuid();
          model = {
            id: currentMeeting,
            type: 2,
            from: new Date(),
            agentId,
            data: JSON.stringify({
              meetingId: request.value.meetingId,
              meetingTitle: request.value.meetingTitle,
            }),
          };
          fetch(url + "/api/workspaceEvent", {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model),
          }).then(r => r.json())
            .then(r => console.log(r));
          break;
      case 'GoogleMeet_Call_End':
          sendResponse({farewell: "GoogleMeet_Call_End2 " + request.value.meetingId});
          fetch(`${url}/api/workspaceEvent/${currentMeeting}`, {
            method: 'put',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
              ...model,
              to: new Date(),
            }),
          }).then(r => r.json())
            .then(r => console.log(r));
          break;
    }
  }
);

class SignalRService {
  private connection: signalR.HubConnection;
  private state: "NotConnected" | "Connecting" | "Connected" = "NotConnected"

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(url + "/Agent?agentId=&type=chromeWidget")
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

function generateGuid() {
  var result, i, j;
  result = '';
  for(j=0; j<32; j++) {
    if( j == 8 || j == 12 || j == 16 || j == 20)
      result = result + '-';
    i = Math.floor(Math.random()*16).toString(16).toUpperCase();
    result = result + i;
  }
  return result;
}

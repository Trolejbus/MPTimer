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

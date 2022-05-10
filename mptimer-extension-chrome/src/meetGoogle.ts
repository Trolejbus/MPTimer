let inCall = false;

setInterval(() => {
    const callDivExist = document.querySelector("[data-layout=roi-crop]") != null;
    if (callDivExist != inCall) {
        if (callDivExist) {
            const dataMeetingTitleDiv = document.querySelector("[data-meeting-title]");
            const dataMeetingIdDiv = document.querySelector("[data-unresolved-meeting-id]");
            const meetingTitle = dataMeetingTitleDiv != null ? dataMeetingTitleDiv.getAttribute('data-meeting-title') : null;
            const meetingId = dataMeetingIdDiv!.getAttribute('data-unresolved-meeting-id');
            chrome.runtime.sendMessage({ type: "GoogleMeet_Call_Started", value: { meetingId, meetingTitle } }, function(response) {
                console.log(response.farewell);
            });
        }
        else {
            const dataMeetingIdDiv = document.querySelector("[data-unresolved-meeting-id]");
            const meetingId = dataMeetingIdDiv!.getAttribute('data-unresolved-meeting-id');
            chrome.runtime.sendMessage({ type: "GoogleMeet_Call_End", value: { meetingId } }, function(response) {
                console.log(response.farewell);
            });
        }

        inCall = callDivExist;
    }
}, 1000);

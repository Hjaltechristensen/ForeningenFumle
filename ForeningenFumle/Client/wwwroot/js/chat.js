window.scrollToBottom = () => {
    var chatBox = document.querySelector(".messages");
    chatBox.scrollTop = chatBox.scrollHeight;
};
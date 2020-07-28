var chatbotFaqSettings = {
    url: "/Chatbot/SendInsuranceFaqMessage",
    method: "POST",
    timeout: 0,
    data: {}
};

function AskChatbot(txtboxId, objectContainer, serviceType) {
    var settings = "";
    if (serviceType.toLowerCase() === "faq") {
        settings = chatbotFaqSettings;
    }
    var contentData = $("#" + txtboxId).val();
    if (contentData !== null && contentData !== "" && contentData !== undefined) {
        var model = { UserQuery: contentData };
        settings.data = model;
        prepareUserChatTemplate(contentData, objectContainer);
        var container = $("#" + objectContainer).html();
        $("#loader").css('display', 'block');
        $.ajax(settings).done(function (response) {
            if (response !== null && response !== undefined && response.chatHistory.length > 0) {
                $("#loader").css('display', 'none');
                $.each(response.chatHistory, function (indx, valu) {
                    //if (valu.sendBy === "user") {
                    //    container = container + prepareUserChatTemplate(valu.message);                   
                    //}
                    if (valu.sendBy === "bot") {
                        container = container + prepareBotChatTemplate(valu.message);
                    }
                });
                $("#" + objectContainer).html(container);
                $("#" + txtboxId).val('');
            }
            //console.log(response);
        });
    }
}

function prepareUserChatTemplate(conversationMessage, objectContainer) {
    var today = new Date();
    var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var template = "<div class='media mb-3'>" +
        "<img src='https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg' alt='user' width='50' class='rounded-circle'> " +
        "<div class='media-body ml-3'> <div class='bg-light rounded py-2 px-3 mb-2'> <p class='text-small mb-0 text-muted'>" +
        conversationMessage +
        "</p > </div > <p class='small text-muted'>" +
        date + " | " + time +
        "</p > </div > </div > ";
    var container = $("#" + objectContainer).html();
    container = container + template;
    $("#" + objectContainer).html(container);
}

function prepareBotChatTemplate(conversationMessage) {
    var today = new Date();
    var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var template = "<div class='media mb-3'>" +
        "<div class='media-body mr-3'> <div class='bg-light rounded py-2 px-3 mb-2'> <p class='text-small mb-0 text-muted'>" +
        conversationMessage +
        "</p > </div > <p class='small text-muted'>" +
        date + " | " + time +
        "</p > </div >" + "<img src='/img/bot.jpg' alt='user' width='50' class='rounded-circle'> "
        + " </div > ";
    return template;
}
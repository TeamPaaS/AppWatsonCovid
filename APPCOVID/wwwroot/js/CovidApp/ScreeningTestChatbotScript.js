var chatbotScrnTstSettings = {
    url: "/Chatbot/SendFaqMessage",
    method: "POST",
    timeout: 0,
    data: {}
};

var objectContainerElement = 'chatContainer';
var txtObjectElement = 'txtChatMsg';

var model = { UserQuery: '' };

function ScreenTest(initType, val) {
    //disabled the buttons
    var existingButtonsList = $(".btn-group button");
    $.each(existingButtonsList, function (indx, valu) {
        valu.setAttribute("disabled", "disabled");
    });
    //end
    var settings = "";
    settings = chatbotScrnTstSettings;
    var container = null;
    if (initType === true) {
        model.UserQuery = "welcome";
        settings.data = model;
        container = $("#" + objectContainerElement).html();
        $("#loader").css('display', 'block');
        CallService(settings, container, val);
    } else {
        var contentData = val === 1 ? 'yes' + window.currentQ : 'no' + window.currentQ;
        model.UserQuery = contentData;
        settings.data = model;
        //prepareUserChatTemplate(contentData, objectContainerElement);
        container = $("#" + objectContainerElement).html();
        $("#loader").css('display', 'block');
        CallService(settings, container, val);
    }
}

function CallService(settings, container) {
    $.ajax(settings).done(function (response) {
        if (response !== null && response !== undefined) {
            if (response == "100") {
                jQuery("#showTestCompleteBtn").css("display", "block");
            } else {
                //window.location = window.location.href;
                if (response.chatHistory!=null && response.chatHistory.length > 0) {
                    $("#loader").css('display', 'none');
                    $.each(response.chatHistory, function (indx, valu) {
                        //if (valu.sendBy === "user") {
                        //    container = container + prepareUserChatTemplate(valu.message);                   
                        //}
                        if (valu.sendBy === "bot") {
                            container = container + prepareBotChatTemplate(valu.message, valu.isOption);
                            window.currentQ = valu.questionNo;
                        }
                    });
                    $("#" + objectContainerElement).html(container);
                    //$("#" + txtObjectElement).val('');
                }
            }
        }
       
        
        //console.log(response);
    });
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

function prepareBotChatTemplate(conversationMessage, isOption) {
    var today = new Date();
    var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    var template = "";
    if (isOption == 1) {
        if (conversationMessage == "" || conversationMessage == null || conversationMessage == undefined) {
            template = 
                "<div class='media mb-3'>" +
        "<img src='https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg' alt='user' width='50' style='margin-right: 10px' class='rounded-circle'> <div class='btn-group' role='group' aria-label='Basic example' style='padding-bottom:5px'> " +
                "<button type='button' onclick='ScreenTest(false,1)' class='btn btn-dark'>Yes</button>" +
                "<button type='button' onclick='ScreenTest(false,0)' class='btn btn-light'>No</button>" +
                "</div></div>";

        } else {
            template = "<div class='media mb-3'>" +
                "<div class='media-body mr-3'> <div class='bg-light rounded py-2 px-3 mb-2'> <p class='text-small mb-0 text-muted'>" +
                conversationMessage +
                "</p > </div > <p class='small text-muted'>" +
                date + " | " + time +
                "</p > </div >" + "<img src='/img/bot.jpg' alt='user' width='50' class='rounded-circle'> </div> " +
                "<div><img src='https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg' alt='user' style='margin-right: 10px' width='50' class='rounded-circle'><div class='btn-group' role='group' style='padding-bottom:5px' aria-label='Basic example' >" +
                "<button type='button' onclick='ScreenTest(false,1)' class='btn btn-dark'>Yes</button>" +
                "<button type='button' onclick='ScreenTest(false,0)' class='btn btn-light'>No</button>" +
                "</div></div>"
        }

    } else {
        template = "<div class='media mb-3'>" +
            "<div class='media-body mr-3'> <div class='bg-light rounded py-2 px-3 mb-2'> <p class='text-small mb-0 text-muted'>" +
            conversationMessage +
            "</p > </div > <p class='small text-muted'>" +
            date + " | " + time +
            "</p > </div >" + "<img src='/img/bot.jpg' alt='user' width='50' class='rounded-circle'> "
            + " </div > ";
    }
    return template;
}


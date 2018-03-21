
    $(document).ready(function() {
        var navListItems = $('div.setup-panel div a'),
            allWells = $('.setup-content'),
            allNextBtn = $('.nextBtn'),
            allPrevBtn = $('.prevBtn');

        allWells.hide();

        navListItems.click(function(e) {
            e.preventDefault();
            var $target = $($(this).attr('href')),
                $item = $(this);

            if (!$item.hasClass('disabled')) {
                navListItems.removeClass('btn-warning').addClass('btn-default');
                $item.addClass('btn-warning');
                allWells.hide();
                $target.show();
                $target.find('input:eq(0)').focus();
            }
        });

        allPrevBtn.click(function() {
            var curStep = $(this).closest(".setup-content"),
                curStepBtn = curStep.attr("id"),
                prevStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().prev()
                    .children("a");

            prevStepWizard.removeAttr('disabled').trigger('click');
        });

        allNextBtn.click(function() {

            var curStep = $(this).closest(".setup-content"),
                curStepBtn = curStep.attr("id"),
                nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next()
                    .children("a"),
                curInputs = curStep.find("input[type='text'],input[type='url'],input[type='file'],select,textarea"),
                isValid = true;

            $(".form-validate").removeClass("has-error");
            for (var i = 0; i < curInputs.length; i++) {
                if (!curInputs[i].validity.valid) {
                    isValid = false;
                    $(curInputs[i]).closest(".form-validate ").addClass("has-error");
                   
                }
            }
            

            if (isValid)
                nextStepWizard.removeAttr('disabled').trigger('click');
        });

        $('div.setup-panel div a.btn-warning').trigger('click');
    });
//////Image Expiriment////
    var filenameImg;
$(function() {
    $("#ExipirimentImg").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = imageIsLoaded;
            reader.readAsDataURL(this.files[0]);
            document.getElementById("txtimg").innerHTML = $('#ExipirimentImg').val();
        }
    });
});

//Image SRC
function imageIsLoaded(e) {
    
    $('#myImg').attr('src', e.target.result);
};
//ComboSearch--------------------------------------------

$("#multiSelectDropDown").chosen({
    width: "100%"

});

///Outils Expiriment Code Query-------------------------------------------------------------
function GetDynamicTextBox(value) {
    var div = $("<div />").attr("class", " input-group mb-3");

    var textBox = $("<input />").attr("type", "text").attr("name", "Outildata")
        .attr("Class", "form-control form-validate col").attr("required", true);
    textBox.addClass("LastTextBox");

    textBox.val(value);
    div.append(textBox);

    var button = $("<input />").attr("type", "button").attr("value", "Supprimer").attr("Class", "btn btn-danger");

    button.attr("onclick", "RemoveTextBox(this)");
    div.append(button);

    return div;
}

function AddTextBox() {
    var x;
    var div = GetDynamicTextBox("");
    var MyContainer = document.querySelector("#TextBoxContainer").innerHTML;
    if (MyContainer !== " ") {
        x = $(".LastTextBox").last().val();
    }
    if (MyContainer === " " || x !== "") {

        $("#TextBoxContainer").append(div);

    }
}

function RemoveTextBox(button) {
    $(button).parent().remove();
}



//Groupe Image Expiriment Inpute--------------------------------------------------------------------------------
function GetDynamicImage(value) {
    var div = $("<div />").attr("class", " input-group mb-3 ");
    var TiTle = $("<input />").attr("type", "text").attr("name", "UploadTitle")
        .attr("class", " form-control col-md-5");
    var Image = $("<input />").attr("type", "file").attr("name", "UploadImage").attr("class", " col-md-4 form-validate").attr("required", true);
    Image.addClass("LastImaegInput");
    Image.attr("accept", "image/x-png,image/gif,image/jpeg");
    TiTle.val(value);
    div.append(TiTle);

    div.append(Image);
    var button = $("<input />").attr("type", "button").attr("value", "Supprimer")
        .attr("Class", "btn btn-danger col-md-3");

    button.attr("onclick", "RemoveImage(this)");
    div.append(button);
    return div;
}

  function  AddGroupImage(){
    var x;
    var div = GetDynamicImage("");
    var MyImageContainer = document.querySelector("#ImageContainner").innerHTML;
    if ($("#FirstImage").val() != "") {

        if (MyImageContainer !== " ") {
            x = $(".LastImaegInput").last().val();
        }
        if (MyImageContainer === " " || x !== "") {

            $("#ImageContainner").append(div);

        }
    }

};

function RemoveImage(button) {
    $(button).parent().remove();
}
//Groupe FileGoogleDrive Expiriment Inpute--------------------------------------------------------------------------------
    function GetDynamicDrive(value) {
        var div = $("<div />").attr("class", " input-group mb-3 ");
        var TiTle = $("<input />").attr("type", "text").attr("name", "UploadTitleDrive")
            .attr("class", " form-control col-md-5");
        var Drive = $("<input />").attr("type", "file").attr("name", "UploadDrive").attr("class", " col-md-4");
        Drive.addClass("LastDriveInput");
        TiTle.val(value);
        div.append(TiTle);

        div.append(Drive);
        var button = $("<input />").attr("type", "button").attr("value", "Supprimer")
            .attr("Class", "btn btn-danger col-md-3");

        button.attr("onclick", "RemoveDrive(this)");
        div.append(button);
        return div;
    }

    $('#AddGroupDrive').click(function () {
        var x;
        var div = GetDynamicDrive("");
        var MyDriveContainer = document.querySelector("#FileContainner").innerHTML;
        if ($("#FirstDrive").val() != "") {

            if (MyDriveContainer !== " ") {
                x = $(".LastDriveInput").last().val();
            }
            if (MyDriveContainer === " " || x !== "") {

                $("#FileContainner").append(div);

            }
        }

    });

    function RemoveDrive(button) {
        $(button).parent().remove();
    }
    //Groupe Conseil Expiriment Inpute--------------------------------------------------------------------------------
    function GetDynamicConsiel(value) {
        var div = $("<div />").attr("class", " input-group mb-3");
        var Conseil = $("<textarea />").attr("name", "ConseilData")
            .attr("class", " form-control form-validate col-12").attr("placeholder", "Conseil").attr("required", true);
        Conseil.addClass("lastConseilInpout");
        Conseil.val(value);
        div.append(Conseil);
        var button = $("<input />").attr("type", "button").attr("value", "Supprimer")
            .attr("Class", "btn btn-danger input-group-prepend");

        button.attr("onclick", "RemoveConseil(this)");
        div.append(button);
        return div;
    }

    function  AddtextBoxConseil(){
        var x;
        var div = GetDynamicConsiel("");
        var MyDriveContainer = document.querySelector("#ConseilContainner").innerHTML;
        if ($("#firstconseil").val() != "") {

            if (MyDriveContainer !== " ") {
                x = $(".lastConseilInpout").last().val();
            }
            if (MyDriveContainer === " " || x !== "") {

                $("#ConseilContainner").append(div);

            }
        }

    };

    function RemoveConseil(button) {
        $(button).parent().remove();
    }
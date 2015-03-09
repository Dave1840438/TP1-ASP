function CharIsAlpha(c) {
    c = c.toLowerCase();
    var alphas = "abcdefghijklmnopqrstuvwxyzàâäçèêëìîïòôöùûüÿñ-";
    return (alphas.indexOf(c, 0) != -1);
}
function ConstrainToAlpha(event) {
    var textBox = (window.event ? window.event.srcElement : event.srcElement || event.target);
    var car = "";
    var code = 0;
    var valide = "";
    var constrain = false;
    for (var c = 0; c < textBox.value.length; c++) {
        car = textBox.value.substr(c, 1);
        if (!CharIsAlpha(car)) {
            if (!constrain)
                alert("Lettres de 'a' a 'z' seulement!");
            constrain = true;
        }
        else {
            valide = valide + car;
        }
    }
    if (constrain)
        textBox.value = valide;
}

function CharIsDigit(c) {
    var digits = "0123456789";
    return (digits.indexOf(c, 0) != -1);
}

function ConstrainToDigit(event) {
    var textBox = (window.event ? window.event.srcElement : event.srcElement || event.target);
    var car = "";
    var code = 0;
    var valide = "";
    var constrain = false;
    for (var c = 0; c < textBox.value.length; c++) {
        car = textBox.value.substr(c, 1);
        if (!CharIsDigit(car)) {
            //if (!constrain)
            //	alert("Chiffre de '0' a '9' seulement!");
            constrain = true;
        }
        else {
            valide = valide + car;
        }
    }
    if (constrain)
        textBox.value = valide;
}

// cette fonction change la couleur de fond d'un input incomplet
function HighLiteIncompleteMaskedInput(event) {
    var textBox = (window.event ? window.event.srcElement : event.srcElement || event.target);
    var masque = textBox.getAttribute("alt");
    mp = textBox.value.length;
    if (textBox.value.length < masque.length)
        textBox.style.backgroundColor = "#FCC";
    else
        textBox.style.backgroundColor = "#FFF";
}
// cette fonction change la couleur de fond d'un input vide
function HighLiteEmptyInput(event) {
    var textBox = (window.event ? window.event.srcElement : event.srcElement || event.target);
    mp = textBox.value.length;
    if (textBox.value.length == 0)
        textBox.style.backgroundColor = "#FCC";
    else
        textBox.style.backgroundColor = "#FFF";
}

function KeyIsDigit(key) {
    return ((48 <= key) && (key <= 57));
}
function KeyIsLetter(key) {
    return ((65 <= key) && (key <= 90));
}
function KeyIsLegalControl(key) {
    return ((key == 8) || (key == 9) || (key == 13) || (key == 16) || (key == 27));
}

function Valide_Masque(event) {
    var key = (window.event ? window.event.keyCode : event.keyCode || event.which);
    var textBox = (window.event ? window.event.srcElement : event.srcElement || event.target);
    var masque = textBox.getAttribute("alt");
    mp = textBox.value.length;
    charTyped = String.fromCharCode(key);
    if (KeyIsLegalControl(key)) {
        if ((mp > 0) && (key == 8)) {// backspace 
            mp--;
            if ((masque[mp] != "C") && (masque[mp] != "#"))
                mp--;
            textBox.value = textBox.value.substr(0, mp);
            return false;
        }
        else {
            textBox.value = textBox.value.toUpperCase();
            return true;
        }
    }
    while ((mp < masque.length) && (masque[mp] != "C") && (masque[mp] != "#")) {
        textBox.value += masque[mp];
        mp++;
    }
    current_pos = mp;
    if (current_pos < masque.length) {
        if ((masque[current_pos] == "C") && (!KeyIsLetter(key)))
            return false;
        if ((masque[current_pos] == "#") && (!KeyIsDigit(key)))
            return false;
    }
    else
        return false;
    textBox.value = textBox.value.toUpperCase();
    return true;
}

function Post_Check_Masque(event) {
    var textBox = (window.event ? window.event.srcElement : event.target);
    var masque = textBox.getAttribute("alt");
    var value = textBox.value;
    var constrain = false;
    var valide_position = 0;
    for (position = 0; position < textBox.value.length; position++) {
        if (masque.charAt(position) == "C") {
            if (!CharIsAlpha(value.substr(position, 1)))
                constrain = true;
        }
        else {
            if (masque.charAt(position) == "#") {
                if (!CharIsDigit(value.substr(position, 1)))
                    constrain = true;
            }
            else
                if (masque.charAt(position) != value.charAt(position))
                    constrain = true;
        }
        if (constrain)
            break;
        else
            valide_position++;
    }
    if (constrain) {
        textBox.value = value.substr(0, valide_position);
    }
    else {
        if (value.length > masque.length) {
            textBox.value = value.substr(0, masque.length);
        }
    }
    textBox.value = textBox.value.toUpperCase();
    var valide = value.length == masque.length;
    return (valide);
}

var cellsIdPrefix = "cell_";

function CellAppendChild(row, col, object) {
    // extraire le pointeur sur la cellule (row, col)
    target = document.getElementById(cellsIdPrefix + row + col);
    if (target) {
        target.appendChild(object);
    }
}
function CellSetText(row, col, text) {
    // extraire le pointeur sur la cellule (row, col)
    target = document.getElementById(cellsIdPrefix + row + col);
    if (target) {
        target.innerHTML = "<span class='label'>" + text + "</psan>";
    }
}
// création de tableau qui recevra les controles du formulaire
// pour inserer tu texte dans une cellule il suffira
// d'utiliser la fonction CellSetText(row, col, value) 
// pour inserer un controle dans une cellule il suffira
// d'utiliser la fonction CellAppendChild(row, col, object) 
function BuildTable(rows, cols) {
    var tableObject = document.createElement("table");
    for (row = 0; row < rows; row++) {
        var rowObject = document.createElement("tr");
        for (col = 0; col < cols; col++) {
            var cellObject = document.createElement("td");
            cellObject.id = cellsIdPrefix + row + col;
            rowObject.appendChild(cellObject);
        }
        tableObject.appendChild(rowObject);
    }
    return tableObject;
}
function AddInputText(row, caption, fieldName, className) {
    // ajout de caption dans la cellule (row, 0)
    CellSetText(row, 0, caption);
    //Création d'un élément input :
    //<input type="text" class="className" id="fieldName" />
    inputObject = document.createElement("input");
    inputObject.setAttribute("id", fieldName);
    inputObject.setAttribute("name", fieldName);
    inputObject.setAttribute("type", "text");
    inputObject.setAttribute("class", className + " CantBeEmpty");
    inputObject.id = fieldName;
    // ajout du de l'objet input dans la cellule (row, 1)
    CellAppendChild(row, 1, inputObject);
}
function AddMaskedInputText(row, caption, fieldName, mask) {
    // ajout de caption dans la cellule (row, 0)
    CellSetText(row, 0, caption);
    //Création d'un élément input :
    //<input type="text" alt="mask" class="masked" id="fieldName" />
    inputObject = document.createElement("input");
    inputObject.setAttribute("id", fieldName);
    inputObject.setAttribute("name", fieldName);
    inputObject.setAttribute("type", "text");
    inputObject.setAttribute("alt", mask);
    inputObject.setAttribute("class", "masked" + " CantBeEmpty");
    inputObject.id = fieldName;
    CellAppendChild(row, 1, inputObject);
}
// Cette fonction construit un groupe de boutons radio HTML 5
// Chaque boutons auront le format suivant
// <div class="radio"> 
//	  <input type="radio" id="name_X" name="name" />
//	  <label for="name_X"> arguments[X] </label>
// </div>
function AddRadioButtonGroup(row, caption, name) {
    // ajout de caption dans la cellule (row, 0)
    CellSetText(row, 0, caption);

    // parcourt des arguments qui suivent row, caption et name
    // exemple: si on fait l'appel suivant
    // AddRadioButtonGroup(row, caption, name, "masculin", "féminin")
    // arguments[0] = row, arguments[1] = caption, arguments[2] = name
    // arguments[3]="masculin" et argumuments[4]="féminin"
    for (i = 3; i < arguments.length; i++) {
        // Création d'un div conteneur du bouton radio
        divObject = document.createElement("div");
        divObject.setAttribute("class", "radio");
        // alternance de couleur de fond
        if (i % 2)
            divObject.style.backgroundColor = "#eee";
        else
            divObject.style.backgroundColor = "#ddd";
        // Création du bouton radio
        radioObject = document.createElement("input");
        radioObject.type = "radio";
        radioObject.name = name;
        radioObject.value = i - 3;
        radioObject.id = name + "_" + (i - 3);
        if (i == 3) {
            // premier bouton du groupe
            // servira éventuellement à repérer un groupe
            // afin de déterminer si un choix a été fait
            radioObject.setAttribute("class", "RadioButtonGroup");
        }

        // Création du label associé au bouton radio
        labelObject = document.createElement("label");
        labelObject.setAttribute("for", radioObject.id);
        labelObject.appendChild(document.createTextNode(arguments[i]));

        // insertion du bouton radio et de son label dans le div 
        divObject.appendChild(radioObject);
        divObject.appendChild(labelObject);

        // insertion du div dans la cellule (row,2)	
        CellAppendChild(row, 1, divObject);
    }
}
function AddSubmitButton(row, caption, action, CauseValidation, confirmMessage) {
    buttonObject = document.createElement("button");
    buttonObject.innerHTML = caption;
    buttonObject.setAttribute("name", "action");
    buttonObject.setAttribute("value", action);
    buttonObject.className="submitBTN";
    CellAppendChild(row, 1, buttonObject);
    if (CauseValidation) { 
        buttonObject.onclick = function () { return CheckForEmptyInput(); }
    } 
    else  {
        if (confirmMessage != null)  {
            buttonObject.onclick = function () { return confirm(confirmMessage);  } 
        }
        else {
            buttonObject.onclick = function () { return true; }
        }
    }
}

function CheckForEmptyInput() {
    
    var canSubmit = true;
    var inputObjects = document.getElementsByClassName("CantBeEmpty");
    for (i = 0; i < inputObjects.length; i++) {
        mask = inputObjects[i].getAttribute("alt");
        if (mask) {
            if (inputObjects[i].value.length < mask.length) {
                canSubmit = false;
            }
        }
        else
            if (inputObjects[i].value.length == 0) {
                inputObjects[i].style.backgroundColor = "#FCC";
                canSubmit = false;
            }
    }
    // chercher tous les groupes de boutons radio
    var radioButtonGroups = document.getElementsByClassName("RadioButtonGroup");
    for (i = 0; i < radioButtonGroups.length; i++) {
        // chercher tous les boutons du même name
        var buttons = document.getElementsByName(radioButtonGroups[i].name);
        oneButtonIsChecked = false;
        // déterminer si un choix a été fait dans le groupe
        for (b = 0; b < buttons.length; b++) {
            if (buttons[b].checked)
                oneButtonIsChecked = true;
        }
        // changer la couleurs des labels du groupe de boutons
        for (b = 0; b < buttons.length; b++) {
            // le label correspondant au bouton on le même parent
            labelObjects = buttons[b].parentNode.getElementsByTagName("label");
            // en principe il n'y a qu'un seul label qui suit le bouton radio
            if (!oneButtonIsChecked) {
                labelObjects[0].style.color = "#F00";
                canSubmit = false;
            }
            else
                labelObjects[0].style.color = "#000";
        }
    }
    return canSubmit;
}

function InstallHighLiteEmptyDelegates() {
    // installation des "delegates"  pour les inputs de classe "ident"
    var inputObjects = document.getElementsByClassName("CantBeEmpty");
    for (i = 0; i < inputObjects.length; i++) {
        inputObjects[i].onkeyup = function () { ConstrainToAlpha(event); };
        inputObjects[i].onblur = function () { HighLiteEmptyInput(event); };
    }

    // installation des "delegates" pour les inputs de classe "masked"
    var maskedInputs = document.getElementsByClassName("masked");
    for (i = 0; i < maskedInputs.length; i++) {
        maskedInputs[i].onkeydown = function () { return Valide_Masque(event); };
        maskedInputs[i].onkeyup = function () { Post_Check_Masque(event); };
        maskedInputs[i].onblur = function () { Post_Check_Masque(event); HighLiteIncompleteMaskedInput(event) };
    }
}


function PreLoadImage(e) {
    var imageTarget = document.getElementById("IMG_Avatar");
    var input = document.getElementById("FU_Avatar");
    if (imageTarget != null) {
        var fReader = new FileReader();
        fReader.readAsDataURL(input.files[0]);
        fReader.onloadend = function (event) {
            // the event.target.result contains the image data 
            imageTarget.src = event.target.result;

        }
    }
    return true;
}

function SetValue(input_ID, value) {
    document.getElementById(input_ID).setAttribute("value", value);
}

function SetRadioButtonGroupValue(rbg_Name, value) {
    var buttons = document.getElementsByName(rbg_Name);
   
    for (b = 0; b < buttons.length; b++) {
        buttons[b].checked = (value == b);       
    }
}

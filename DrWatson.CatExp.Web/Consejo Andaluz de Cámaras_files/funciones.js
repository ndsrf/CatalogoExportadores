function ValidarCaracteres(textareaControl, maxlength) {
    if (textareaControl.value.length >= maxlength) {
        textareaControl.value = textareaControl.value.substring(0, maxlength);
        alert("Debe teclear hasta un maximo de " + maxlength + " caracteres");
    }
}    
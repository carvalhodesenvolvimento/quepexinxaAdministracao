var screenWidth, screenHeight;
screenWidth = screen.availWidth;
screenHeight = screen.availHeight;

var features;
var arrPopup = new Array();
var index = -1;

function abrirPopup(url, name, width, height)
{
    popWidth = width;
    popHeight = height;
    popLeft = (screenWidth - popWidth)/2;
    popTop = (screenHeight - popHeight)/2;
    
    features = 'directories=0,location=0,menubar=0,toolbar=0,status=0,statusbar=0,resizable=0,scrollbars=0,dependent=0,innerWidth=' + popWidth + ',width=' + popWidth + ',innerHeight=' + popHeight + ',height=' + popHeight + ',left=' + popLeft + ',top=' + popTop;

    index ++;
    arrPopup[index] = openWindow(url, name, features).focus();
}

function abrirPopup2(url, name, width, height) {
    popWidth = width;
    popHeight = height;
    popLeft = (screenWidth - popWidth) / 2;
    popTop = (screenHeight - popHeight) / 2;

    features = 'directories=0,location=0,menubar=0,toolbar=0,status=0,statusbar=0,resizable=0,scrollbars=yes,dependent=0,innerWidth=' + popWidth + ',width=' + popWidth + ',innerHeight=' + popHeight + ',height=' + popHeight + ',left=' + popLeft + ',top=' + popTop;

    index++;
    arrPopup[index] = openWindow(url, name, features).focus();
}

function abrirPopupRedimensionavel(url, name, width, height)
{
    popWidth = width;
    popHeight = height;
    popLeft = (screenWidth - popWidth)/2;
    popTop = (screenHeight - popHeight)/2;
    
    features = 'directories=0,location=0,menubar=0,toolbar=0,status=0,statusbar=0,resizable=yes,scrollbars=yes,dependent=0,innerWidth=' + popWidth + ',width=' + popWidth + ',innerHeight=' + popHeight + ',height=' + popHeight + ',left=' + popLeft + ',top=' + popTop;

    index ++;
    arrPopup[index] = openWindow(url, name, features).focus();
}

function fecharPopups()
{
    for (i=0;i<=index;i++)
    {
        if (arrPopup[i] != null)
        {
            win = arrPopup[i];
            win.close();
        }
    }
}

function openWindow(url, name, features)
{
    return window.open(url, name, features);
}

//Efeitos de mudança de cor da linha no grid ao passar o mouse
var cor;
var cursor = 'pointer';
var bgColor = '#D1DDF1';
    
function mouseOver(obj, changeCursor) 
{   
    cor = obj.style.backgroundColor;
    obj.style.backgroundColor = bgColor;
    
    if (changeCursor) 
        obj.style.cursor = cursor;
}

function mouseOut(obj) 
{
    obj.style.backgroundColor = cor;
}

function ExibirMensagem(mensagem)
{
    alert(mensagem);
}

function ValidarFormulario(campoID, controleID, nomeCampo) {
    if (document.getElementById(campoID).value == '') {
        document.getElementById(controleID).innerHTML = 'É necessário preencher o campo ' + nomeCampo;
        document.getElementById(campoID).focus();
        return false;
    }
    return true;
}

function IsMaxLength(objeto, tamanho){
    if (objeto.value.length > tamanho)
        objeto.value = objeto.value.substring(0, tamanho);
}

function ExibirMensagem(_campoID, _campoMensagemID, _mensagemID) {
    document.getElementById(_campoMensagemID).innerHTML = _mensagemID;
    LimparMensagem(_campoID, _campoMensagemID)
    return false;
}

function ValorCampo(_campoID) {
    return document.getElementById(_campoID).value;
}

function LimparMensagem(_campoMensagemID) {
    setTimeout(function () { document.getElementById(_campoMensagemID).innerHTML = ""; }, 5000);
}

function LimparMensagem(_campoID, _campoMensagemID) {
    setTimeout(function () { document.getElementById(_campoMensagemID).innerHTML = ""; document.getElementById(_campoID).focus(); }, 5000);
}

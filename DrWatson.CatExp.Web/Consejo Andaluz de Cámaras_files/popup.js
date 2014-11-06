/***************************/
//@Author: Adrian "yEnS" Mato Gondelle
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

//SETTING UP OUR POPUP
//0 means disabled; 1 means enabled;
var popupStatus = 0;

//loading popup with jQuery magic!
function loadPopup(id){
	//loads popup only if it is disabled
	if(popupStatus==0){
		$("#backgroundPopup").css({
			"opacity": "0.7"
		});
		$("#backgroundPopup").fadeIn("slow");
		$(id).fadeIn("slow");
		popupStatus = 1;
	}
}

//disabling popup with jQuery magic!
function disablePopup(id){
	//disables popup only if it is enabled
	if(popupStatus==1){
		$("#backgroundPopup").fadeOut("slow");
		$(id).fadeOut("slow");
		popupStatus = 0;
	}
}

//centering popup
function centerPopup(id){
	//request data for centering
	var windowWidth = document.documentElement.clientWidth;
	var windowHeight = document.documentElement.clientHeight;
	var popupHeight = $(id).height();
	var popupWidth = $(id).width();
	//centering
	$(id).css({
		"position": "absolute",
		"top": windowHeight/2-popupHeight/2,
		"left": windowWidth/2-popupWidth/2
	});
	//only need force for IE6
	
	$("#backgroundPopup").css({
		"height": windowHeight
	});
	
}

function abrirPopup(id)
{
	centerPopup(id);
	loadPopup(id);
}



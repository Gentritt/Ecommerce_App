$(document).ready(function () {


	var theform = $("#the-form");
	theform.hide();

	var button = $("#buyButton"); //Gets the log whenever the Buy button is clicked 
	button.on("click", function () {
		console.log("Buying Item");
	});

	var productinfo = $(".product-props li"); //Geths the log of the items i clicked 
	productinfo.on("click", function () {
		console.log("You clicked" + $(this).text());
	});


	var loginToggle = $("#logginToggle");
	var popupForm = $(".popup-form");

	loginToggle.on("click", function () { //Toggles the form Hide/Show
		popupForm.fadeToggle(1000);

	})
});
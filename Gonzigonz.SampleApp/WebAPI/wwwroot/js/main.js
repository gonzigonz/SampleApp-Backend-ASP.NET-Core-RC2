$(document).ready(function () {

	var siteUrl = window.location.href;

	var liveUrlLink = $("#liveUrl");
	liveUrlLink.attr("href", siteUrl + "api/todo");
	liveUrlLink.text(siteUrl + "api/todo:");

	$.get("/api/todo", function (data) {
		$("#liveTodoItems").text(JSON.stringify(data, null, 2));
	});

});

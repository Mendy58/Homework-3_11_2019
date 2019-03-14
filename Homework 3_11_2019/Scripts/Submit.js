$(() => {
	$("#npost").on('keyup', () => {
		const enable = $("#phone").val().length === 10 && $("#Name").val().length > 1 && $("#Des").val().length > 6 ;
		$("#submit").prop('disabled', !enable);
	});
});
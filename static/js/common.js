$(function () {
	/* data-hover="navbar" */ 
	$('[data-hover="navbar"]').hover(function () {	
		var $height = $(this).find('.dropdown-menu').outerHeight();		
		$(this).addClass('open');
		$(this).append('<div class="dropdown-backdrop"></div>')
		$('.dropdown-backdrop').height($height).width($(window).width()).show();		
	}, function () {
		$(this).removeClass('open');
		$('.dropdown-backdrop').remove();		
	});		
});

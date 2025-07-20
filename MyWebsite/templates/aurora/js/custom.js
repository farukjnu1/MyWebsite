jQuery(function ($) {
	
// POINTER CHANGE

$(document).on('mousemove', function(e) {
  $('#circle-big').css({
    left: e.pageX,
    top: e.pageY
  });
  $('#circle').css({
    left: e.pageX,
    top: e.pageY
  });
});
$('a').mouseover(function() {
  $('#cursor').addClass('on-link');
})
$('a').mouseout(function() {
  $('#cursor').removeClass('on-link');
})	

// END POINTER CHANGE

});
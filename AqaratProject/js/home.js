$(document).ready(function() {
  $('.right').click(function() {
    var position = $('.container').position();
    var r = position.left - $(window).width()
    $('.container').animate({
      'left': '' + r + 'px'
    });
  });

  $('.left').click(function() {
    var position = $('.container').position();
    var l = position.left + $(window).width()
    if (l <= 0) {
      $('.container').animate({
        'left': '' + l + 'px'
      });
    }
  });
});
var length = $('div .container').children('div .move').length;
$(".articles .container").width(length * 100 + '%');

function fun() {
    if (confirm("Are You Sure That U Want To Delete")) {
        console.log("Dleted");
    } else {
        console.log("Not Deleted");
    }
}
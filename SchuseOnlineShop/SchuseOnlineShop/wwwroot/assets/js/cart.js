// SIDEBAR
let iconx = document.querySelector(".iconx");
let icon = document.querySelector(".fa-bars");
let overlay = document.querySelector(".overlay");

icon.onclick = function () {
  var sidebar = document.querySelector(".content-area");
  sidebar.style.width = "65%";  
  sidebar.style.visibility = "visible";
  sidebar.style.opacity = "1";
};

iconx.onclick = function () {
  var sidebar = document.querySelector(".content-area");
  sidebar.style.width = "0%";
  sidebar.style.visibility = "hidden";
  sidebar.style.opacity = "0";
};


//Sticky--menu 

$(function(){
  "use strict";

  // Sticky menu 
  var $window = $(window);
  $window.on('scroll', function () {
    var scroll = window.scrollY;
    if (scroll < 200) {
      $(".down-navbar").removeClass("stick-nav");
    } else {
      $(".down-navbar").addClass("stick-nav");
    }
  });


})


// ----count-plus-minis----
$(document).ready(function() {
  $('.minus').click(function () {
    var $input = $(this).parent().find('input');
    var count = parseInt($input.val()) - 1;
    count = count < 1 ? 1 : count;
    $input.val(count);
    $input.change();
    return false;
  });
  $('.plus').click(function () {
    var $input = $(this).parent().find('input');
    $input.val(parseInt($input.val()) + 1);
    $input.change();
    return false;
  });
});



$(document).ready(function () {
  //Bir-başa headerə qaytarn icon
  // scroll to top
  $(window).on('scroll', function () {
    if ($(this).scrollTop() > 600) {
      $('.scroll-top').removeClass('not-visible');
    } else {
      $('.scroll-top').addClass('not-visible');
    }
  });
  $('.scroll-top').on('click', function (event) {
    $('html,body').animate({
      scrollTop: 0
    }, 1000);
  });


});


// -----------

$(function () {

    $(document).on("click", ".delete-product", function () {

        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        let tbody = $(".tbody").children();
        console.log(tbody)
        let data = { id: id };

        $.ajax({
            type: "Post",
            url: `Cart/DeleteDataFromBasket`,
            data: data,
            success: function () {
                if ($(tbody).length == 1) {
                    $(".product-table").addClass("d-none");
                    //$(".footer-alert").removeClass("d-none")
                }
                $(prod).remove();
                grandTotal();
            }
        })
        return false;
    })




    function grandTotal() {
        let tbody = $(".tbody").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(4).text())
            sum += price
        }
        $(".grand-total").text(sum);

    }



})


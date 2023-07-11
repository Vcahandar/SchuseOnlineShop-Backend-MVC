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


// -----------delete-cart-----------

$(function () {

    $(document).on("click", ".delete-product", function () {

        var id = $(this).data('id')
        var basketCount = $('.count-bask')
        var basketCurrentCount = $('.count-bask').html()
        let tbody = $(".tbody").children();
        var quantity = $(this).data('quantity')
        var sum = basketCurrentCount - quantity

        $.ajax({
            method: 'POST',
            url: "/cart/delete",
            data: {
                id: id
            },
            success: function (res) {
                if ($(tbody).length == 1) {
                    $(".product-table").addClass("d-none");
                    //$(".footer-alert").removeClass("d-none")
                }

                $(`.basket-product[data-id=${id}]`).remove();
                basketCount.html("")
                basketCount.append(sum)
                grandTotal();

            }
        })
    })



    function grandTotal() {
        let tbody = $(".tbody").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(4).children().eq(0).text())
     
            console.log(price)
            sum += price
        }

        $(".grand-total").text(sum);

    }


    function subTotal(res, nativePrice, total, count) {
        $(count).val(res);
        let subtotal = parseFloat(nativePrice * $(count).val());
        $(total).text(subtotal + ".00");
    }




})

//$(function () {

//    //change product count
//    $(document).on("click", ".inc", function () {
//        let id = $(this).parent().parent().parent().attr("data-id");
//        console.log("dsd")
//        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
//        let total = $(this).parent().parent().next().children().eq(1);
//        let count = $(this).prev().prev();

//        $.ajax({
//            type: "Post",
//            url: `Cart/IncrementProductCount?id=${id}`,
//            success: function (res) {
//                res++;
//                subTotal(res, nativePrice, total, count)
//                grandTotal();
//            }
//        })
//    })
//})




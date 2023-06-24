// SIDEBAR
let iconx = document.querySelector(".iconx");
let icon = document.querySelector(".fa-bars");
let overlay = document.querySelector(".overlay");

icon.onclick = function () {
  var sidebar = document.querySelector(".content-area");
  sidebar.style.width = "80%";  
  sidebar.style.visibility = "visible";
  sidebar.style.opacity = "1";
};

iconx.onclick = function () {
  var sidebar = document.querySelector(".content-area");
  sidebar.style.width = "0%";
  sidebar.style.visibility = "hidden";
  sidebar.style.opacity = "0";
};



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

//------------------------------





//-------price filtre --star------------

let minValue = document.getElementById("min-value");
//console.log(minValue);
let maxValue = document.getElementById("max-value");

function validateRange(minPrice, maxPrice) {
  if (minPrice > maxPrice) {
    // Swap to Values
    let tempValue = maxPrice;
    maxPrice = minPrice;
    minPrice = tempValue;
  }

  minValue.innerHTML = "$" + minPrice;
  maxValue.innerHTML = "$" + maxPrice;
}

const inputElements = document.querySelectorAll(".range-slider input");
inputElements.forEach((element) => {
  element.addEventListener("change", (e) => {
    let minPrice = parseInt(inputElements[0].value);
    let maxPrice = parseInt(inputElements[1].value);

    validateRange(minPrice, maxPrice);
  });
});

validateRange(inputElements[0].value, inputElements[1].value);

//-------price filtre --end------------


$('.pagination-inner a').on('click', function () {
   
  $(this).siblings().removeClass('pagination-active');
  $(this).addClass('pagination-active');
})




//-----------modal-start-----------------

let icons = document.querySelectorAll(".card .product-hover  .fa-eye");

let cards = document.querySelectorAll(".card");

let modal = document.querySelector(" .mini-modal .product-modal");

let modalImg = document.querySelector(" .mini-modal .product-modal .img img")

for (const icon of icons) {
  icon.addEventListener("click", function () {

    document.querySelector(".mini-modal").style.display = "block"
    document.querySelector(".product-modal").classList.remove("d-none")
    document.querySelector(".mini-modal").classList.remove("d-none")
    document.querySelector("#overlay").style.display = "block";
    document.body.style.overflow = "hidden"

    let prodImg = this.parentNode.parentNode.parentNode.firstElementChild.firstElementChild.getAttribute("src");

    let prodName = this.parentNode.parentNode.nextElementSibling.nextElementSibling.firstElementChild.firstElementChild.innerText;
    let prodPrice = this.parentNode.parentNode.nextElementSibling.nextElementSibling.children[1].innerText;
    let proDiscount = this.parentNode.parentNode.nextElementSibling.nextElementSibling.lastElementChild.innerText;


    document.querySelector(".product-modal .img img").setAttribute("src", prodImg);


    document.querySelector(".prod-tittle h2").innerText = prodName;
    document.querySelector(".price-discount .discount del").innerText = proDiscount;
    document.querySelector(".price-discount  .pro-price p").innerText = prodPrice;

  })
}


let iconDelete = document.querySelector(".product-modal .delete-icon a i");
iconDelete.addEventListener("click", function (e) {
  e.preventDefault();
  document.querySelector(".mini-modal").classList.add("d-none")
  document.querySelector(".product-modal").classList.add("d-none")
  document.querySelector("#overlay").style.display = "none";
  document.body.style.overflow = "unset"
})

window.addEventListener("click", function (e) {
  if (e.target == document.querySelector(".mini-modal")) {
    document.querySelector(".mini-modal").classList.add("d-none")
    document.querySelector(".product-modal").classList.add("d-none");
    document.querySelector("#overlay").style.display = "none";
    this.document.body.style.overflow = "unset"
  }
})



$(document).ready(function () {
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

// --------------- Modul END --------------------







//product-slider---- Start ----------

const sliders = [...document.querySelectorAll(".slider__container")];
const sliderControlPrev = [...document.querySelectorAll(".slider__control.prev")];
const sliderControlNext = [...document.querySelectorAll(".slider__control.next")];

sliders.forEach((slider, i) => {
  let isDragStart = false,
    isDragging = false,
    isSlide = false,
    prevPageX,
    prevScrollLeft,
    positionDiff;

  const sliderItem = slider.querySelector(".slider__item");
  var isMultislide = (slider.dataset.multislide === 'true');

  sliderControlPrev[i].addEventListener('click', () => {
    if (isSlide) return;
    isSlide = true;
    let slideWidth = isMultislide ? slider.clientWidth : sliderItem.clientWidth;
    slider.scrollLeft += -slideWidth;
    setTimeout(function () { isSlide = false; }, 700);
  });

  sliderControlNext[i].addEventListener('click', () => {
    if (isSlide) return;
    isSlide = true;
    let slideWidth = isMultislide ? slider.clientWidth : sliderItem.clientWidth;
    slider.scrollLeft += slideWidth;
    setTimeout(function () { isSlide = false; }, 700);
  });

  function autoSlide() {
    if (slider.scrollLeft - (slider.scrollWidth - slider.clientWidth) > -1 || slider.scrollLeft <= 0) return;
    positionDiff = Math.abs(positionDiff);
    let slideWidth = isMultislide ? slider.clientWidth : sliderItem.clientWidth;
    let valDifference = slideWidth - positionDiff;
    if (slider.scrollLeft > prevScrollLeft) {
      return slider.scrollLeft += positionDiff > slideWidth / 5 ? valDifference : -positionDiff;
    }
    slider.scrollLeft -= positionDiff > slideWidth / 5 ? valDifference : -positionDiff;
  }

  function dragStart(e) {
    if (isSlide) return;
    isSlide = true;
    isDragStart = true;
    prevPageX = e.pageX || e.touches[0].pageX;
    prevScrollLeft = slider.scrollLeft;
    setTimeout(function () { isSlide = false; }, 700);
  }

  function dragging(e) {
    if (!isDragStart) return;
    e.preventDefault();
    isDragging = true;
    slider.classList.add("dragging");
    positionDiff = (e.pageX || e.touches[0].pageX) - prevPageX;
    slider.scrollLeft = prevScrollLeft - positionDiff;
  }

  function dragStop() {
    isDragStart = false;
    slider.classList.remove("dragging");
    if (!isDragging) return;
    isDragging = false;
    autoSlide();
  }

  addEventListener("resize", autoSlide);
  slider.addEventListener("mousedown", dragStart);
  slider.addEventListener("touchstart", dragStart);
  slider.addEventListener("mousemove", dragging);
  slider.addEventListener("touchmove", dragging);
  slider.addEventListener("mouseup", dragStop);
  slider.addEventListener("touchend", dragStop);
  slider.addEventListener("mouseleave", dragStop);
});


//product Slider ---END -------------





$(document).ready(function () {
  $('.customer-logos').slick({
    slidesToShow: 6,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 1500,
    arrows: false,
    dots: false,
    pauseOnHover: false,
    responsive: [{
      breakpoint: 768,
      settings: {
        slidesToShow: 4
      }
    }, {
      breakpoint: 520,
      settings: {
        slidesToShow: 3
      }
    }]
  });


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



//--------Modul START-----------------


let icons = document.querySelectorAll(".card .product-hover  .fa-eye");

let cards = document.querySelectorAll(".card");

let modal = document.querySelector("#recent-arrivals .mini-modal .product-modal");

let modalImg = document.querySelector("#recent-arrivals .mini-modal .product-modal .img img")

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

$(function () {
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



// --------------- Modul END --------------------


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




//----------icon-whislist-start------------

let bookIcons = document.querySelectorAll(" .card .product-hover .fa-bookmark")


bookIcons.forEach(icons => {
  icons.addEventListener("click", function () {
 if (icons.classList.contains("fa-regular")) {
  icons.classList.remove("fa-regular");
  icons.classList.add("fa-solid");
 }else{
  icons.classList.remove("fa-solid");
  icons.classList.add("fa-regular");
 }

    
  })

  
});


$(function () {

    //add cart
    AddToCart(".add-to-cart-btn", "/cart/AddCart");

    function AddToCart(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            let id = $(this).attr("data-id");
            let data = { id: id };
            let count = (".count-bask");
            $.ajax({
                type: "Post",
                url: url,
                data: data,
                success: function (res) {
                    $(count).text(res);
                }
            })
            return false;
        })
    }


})

$(function () {

    //add wishlist
    AddToCart(".add-to-wishlist-btn", "/Shop/AddToWishlist");

    function AddToCart(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            let id = $(this).attr("data-id");
            let data = { id: id };
            let count = (".count-whislist");
            $.ajax({
                type: "Post",
                url: url,
                data: data,
                success: function (res) {
                    $(count).text(res);
                }
            })
            return false;
        })
    }




})




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


const togglePassword = document.querySelector("#login-page .password-input .eyes");
const password = document.querySelector("#login-page .password-input input");


togglePassword.addEventListener("click", function () {
  // toggle the type attribute
  const type = password.getAttribute("type") === "password" ? "text" : "password";
  password.setAttribute("type", type);

  // // toggle the icon
  this.classList.toggle("fa-eye");
});

//// prevent form submit
//const form = document.querySelector("form");
//form.addEventListener('submit', function (e) {
//  e.preventDefault();
//});

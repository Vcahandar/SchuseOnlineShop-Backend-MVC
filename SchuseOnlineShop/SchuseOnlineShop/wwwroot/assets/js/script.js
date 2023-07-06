"use strict" 


$(function () {


    $(document).on("submit", ".hm-searchbox", function () {
        let value = $(".input-search").val();
        let url = `/Shop/Search?searchText=${value}`;
        console.log(url)
        window.location.assign(url);
        return false;
    })
})





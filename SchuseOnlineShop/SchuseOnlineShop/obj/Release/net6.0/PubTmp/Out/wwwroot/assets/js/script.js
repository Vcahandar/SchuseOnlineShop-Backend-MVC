"use strict" 


$(function () {


    $(document).on("submit", ".hm-searchbox", function () {
        let value = $(".input-search").val();
        let url = `/Shop/Search?searchText=${value}`;
        console.log(url)
        window.location.assign(url);
        return false;
    })



    $(document).on("submit", "#filterForm", function (e) {
        e.preventDefault();
        let value1 = $(".min-price").val()
        let value2 = $(".max-price").val()
        console.log(value1)
        console.log(value2)

        let data = { value1: value1, value2: value2 }
        let parent = $(".pro-list")
        $.ajax({
            url: "/Shop/GetRangeProducts",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);
            }

        })

    })
})








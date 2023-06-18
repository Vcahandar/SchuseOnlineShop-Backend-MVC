$(function () {
    //delete 

    RemoveItem(".delete-slider", "/Admin/Slider/Delete");
    RemoveItem(".delete-brandlogo", "/Admin/BrandLogo/Delete");
    RemoveItem(".delete-advert", "/Admin/Advert/Delete");
    RemoveItem(".delete-category", "/Admin/Category/Delete");
    RemoveItem(".delete-size", "/Admin/Size/Delete");
    RemoveItem(".delete-testimonial", "/Admin/Testimonial/Delete");
    RemoveItem(".delete-color", "/Admin/Color/Delete");
    RemoveItem(".delete-tag", "/Admin/Tag/Delete");
    RemoveItem(".delete-banner", "/Admin/Banner/Delete");
    RemoveItem(".delete-brand","/Admin/Brand/Delete");
    RemoveItem(".delete-blog", "/Admin/Blog/Delete");
    RemoveItem(".delete-product", "/Admin/Product/Delete");
    RemoveItem(".delete-productComment", "/Admin/ProductComment/Delete");
    RemoveItem(".delete-blogComment", "/Admin/ProductComment/Delete");



    SetStatus("/Admin/Blog/SetStatus");
    SetStatus("/Admin/Product/SetStatus");

    RemoveImage("/Admin/Blog/DeleteBlogImage");
    RemoveImage("/Admin/Product/DeleteProductImage");


    function RemoveItem(clickedElem,url) {
        $(document).on("click", clickedElem , function (e) {
            e.preventDefault()
            let deleteElem = $(this).parent().parent();
            let id = $(this).parent().parent().attr("data-id");
            let data = { id: id };
            let tbody = $(deleteElem).parent().children();
            $.ajax({
                url: url,
                type: "Post",
                data: data,
                success: function () {
                    if ($(tbody).length == 1) {
                        $(".table").remove();
                        (".paginate-area").remove();
                    }
                    for (let item of deleteElem) {
                        if ($(item).attr("data-id") == id) {
                            $(item).remove();
                            $(".tooltip-inner").remove()
                            $(".arrow").remove()
                        }
                    }
                }
            })
        })
    }
    function RemoveImage(url) {
        $(document).on("click", ".delete-image", function (e) {
            e.preventDefault()
            let deleteItem = $(this).parent().parent();
            let imageId = $(this).parent().attr("data-id");
            let data = { id: imageId }

            $.ajax({
                url: url,
                type: "Post",
                data: data,
                success: function (res) {
                    if (res.result) {
                        debugger
                        $(deleteItem).remove();
                        let imagesId = $(".images").children().eq(0).attr("data-id");
                        let data = $(".images").children().eq(0);
                        let changeElem = $(data).children().eq(1).children().eq(1);

                        if (res.id == imagesId) {
                            if ($(changeElem).children().hasClass("de-active")) {
                                $(changeElem).children().eq(0).addClass("active-status");
                                $(changeElem).children().eq(0).removeClass("de-active");
                            }
                        }
                    }
                    else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'success',
                            title: 'Image must be minimum one',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }


                }
            })
        })
    }
    function SetStatus(url) {
        $(document).on("click", ".statuses .image-status", function () {
            let imageId = $(this).parent().parent().attr("data-id");
            let elems = $(".image-status")
            let changeElem = $(this);
            let data = { id: imageId }
            $.ajax({
                url: url,
                type: "Post",
                data: data,
                success: function (res) {
                    if (res) {
                        for (var elem of elems) {
                            if ($(elem).hasClass("active-status")) {
                                $(elem).removeClass("active-status")
                                $(elem).addClass("de-active")
                            }
                            if ($(changeElem).hasClass("active-status")) {
                                Swal.fire({
                                    position: 'top-end',
                                    icon: 'success',
                                    title: 'One picture must be the main',
                                    showConfirmButton: false,
                                    timer: 1500
                                })
                            }
                        }
                        if ($(changeElem).hasClass("de-active")) {
                            $(changeElem).removeClass("de-active");
                            $(changeElem).addClass("active-status");
                        }
                    }
                }
            })
        })
    }
})
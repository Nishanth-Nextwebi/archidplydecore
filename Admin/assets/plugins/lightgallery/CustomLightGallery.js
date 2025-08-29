//   lightGallery------------------
$(".image-popup").lightGallery({
    selector: "this",
    cssEasing: "cubic-bezier(0.25, 0, 0.25, 1)",
    download: false,
    counter: false
});
var o = $(".lightgallery"),
    p = o.data("looped");
o.lightGallery({
    selector: ".lightgallery a.popup-image",
    cssEasing: "cubic-bezier(0.25, 0, 0.25, 1)",
    download: false,
    loop: false,
    counter: false
});
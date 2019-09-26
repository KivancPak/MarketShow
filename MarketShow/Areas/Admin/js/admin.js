
// https://stackoverflow.com/a/4459419
// inputSelector: Resim dosyasının okunacağı input
// imgSelector: Resmin önizleneceği img
function ResimOnizle(inputSelector, imgSelector) {
    var input = $(inputSelector);
    var img = $(imgSelector);
    var oldSrc = img.attr('src');

    input.change(function () {

        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                img.attr('src', e.target.result);
            }

            reader.readAsDataURL(this.files[0]);
        }
        else {  
            img.attr('src', oldSrc);
        }
    });
}
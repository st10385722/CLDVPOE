function searchProducts(searchTerm) {
    var productList = $(".container .row"); 
    productList.empty();

    var allProducts = $("#allProducts").children(); // Get all product elements

    if (searchTerm) { // Filter products if there's a search term
        allProducts.each(function () {
            var productCategory = $(this).find(".card-title").text().toLowerCase();
            if (productCategory.includes(searchTerm.toLowerCase())) {
                productList.append($(this).clone());
            }
        });
    } else { // No search term, show all products
        allProducts.clone().appendTo(productList);
    }
}




(function () {

    //'use strict';

    $(document).ready(function () {

        function sortBy($list, sorting, type) {

            var products = $list.find(".product");

            if (sorting == "price") {
                products = sortByPrice(products, type);
            } else if (sorting == "popularity") {
                products = sortByPopularity(products, type);
            }

            var ul = $(".products");
            ul.empty();
            ul.append(products);

            var urlStr = window.location.href.split('?')[0] + "?sorting=" + sorting + "&sortingType=" + type;
            window.history.pushState("", "", urlStr);

        }

        function sortByPrice($list, type) {

            if (type === "asc") {
                $list.sort(function (a, b) {
                    aval = $(a).find(".price-availability .price span")[1];

                    bval = $(b).find(".price-availability .price span")[1];
                    if (parseFloat(aval.textContent) > parseFloat(bval.textContent)) {
                        return 1;
                    }
                    else if (aval == bval) {
                        return 0;
                    }
                    else {
                        return -1;
                    }
                });
            } else if (type === "desc") {
                $list.sort(function (a, b) {
                    aval = $(a).find(".price-availability .price span")[1];

                    bval = $(b).find(".price-availability .price span")[1];
                    if (parseFloat(aval.textContent) < parseFloat(bval.textContent)) {
                        return 1;
                    }
                    else if (aval == bval) {
                        return 0;
                    }
                    else {
                        return -1;
                    }
                });
            }


            return $list;
        }

        function sortByPopularity($list, type) {

            if (type === "asc") {
                $list.sort(function (a, b) {
                    aval = $(a).attr("data-popularity");

                    bval = $(b).attr("data-popularity");
                    if (parseFloat(aval) > parseFloat(bval)) {
                        return 1;
                    }
                    else if (aval == bval) {
                        return 0;
                    }
                    else {
                        return -1;
                    }
                });
            } else if (type === "desc") {
                $list.sort(function (a, b) {
                    aval = $(a).attr("data-popularity");

                    bval = $(b).attr("data-popularity");
                    if (parseFloat(aval) < parseFloat(bval)) {
                        return 1;
                    }
                    else if (aval == bval) {
                        return 0;
                    }
                    else {
                        return -1;
                    }
                });
            }

            return $list;

        }

        function successCallback(res) {

            var products = JSON.parse(res.d);

            for (i = 0; i < products.length; i++) {
                var container = $("#" + products[i].id).children(".price-availability");
                container.children(".price").append("<span>" + products[i].price + "</span>");
                container.children(".availability").append("<span>" + products[i].availability + "</span>");
            }

        }

        $.ajax({
            type: "GET",
            url: "Details.aspx/GetProductsPriceAndAvailablity",
            contentType: "application/json",
            success: function (response) {
                successCallback(response);
            },
            failure: function (response) {
                alert(response.d);
            }
        });

        var vars = [];
        var hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        var productsList = [];
        var defaultSortingType = "asc";

        productsList = $(".products");

        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }

        if (vars.length > 0) {
            sortBy(productsList, vars["sorting"], vars["sortingType"]);
        }

        $("#ascSorting").click(function () {
            sortBy(productsList, $('#sortByBox').find(":selected").attr("value"), "asc");
        });

        $("#descSorting").click(function () {
            sortBy(productsList, $('#sortByBox').find(":selected").attr("value"), "desc");
        });

        $("#sortByBox").change(function () {
            sortBy(productsList, $('#sortByBox').find(":selected").attr("value"), defaultSortingType);
        });

    });

})();
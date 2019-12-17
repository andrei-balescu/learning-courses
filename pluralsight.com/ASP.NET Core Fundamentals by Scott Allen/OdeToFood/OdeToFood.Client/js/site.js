// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

pages = {};

pages.clientRestaurants = function(){
    function getCuisines(){
        return $.ajax("/api/cuisines", { method: "get" }); // returns a promise
    }

    function getRestaurants(){
        return $.ajax("/api/restaurants", { method: "get" });
    }

    function displayRestaurants(cuisines, restaurants)
    {
        $("#restaurants").dataTable({
            data: restaurants,
            columns: [
                { data: "name" },
                { data: "location" },
                { 
                    data: "cuisine", 
                    render: function(data) {
                        return cuisines[data];
                    }
                }
            ]
        });
    }

    $.when(getCuisines(), getRestaurants()).done(function(cuisineResponse, restaurantResponse){
        console.log("received cuisines: ", cuisineResponse);
        console.log("received restaurants: ", restaurantResponse);
        displayRestaurants(cuisineResponse[0], restaurantResponse[0]);
    });
}
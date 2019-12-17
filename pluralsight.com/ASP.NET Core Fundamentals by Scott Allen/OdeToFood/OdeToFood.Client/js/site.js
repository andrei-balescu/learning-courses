// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

pages = {};

pages.clientRestaurants = function(){
    var cuisines = ["Unknown", "Mexican", "Italian", "Indian"];

    $.ajax("/api/restaurants", 
        { method: "get" }) // returns a promise
    .then(function(response){
        // populate table using dataTable plugin
        $("#restaurants").dataTable({
            data: response,
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
    });
}
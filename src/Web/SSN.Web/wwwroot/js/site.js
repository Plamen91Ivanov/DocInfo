// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


 
    $(document).ready(function(){
        $('[data-toggle="popover"]').popover();
});
 


    var userBtn = document.getElementById('user-btn');
    var doctorBtn = document.getElementById('doctor-btn')



userBtn.addEventListener('click', () => {

    const testEl = document.getElementById('user');

    testEl.style.display = 'block';
     
});

doctorBtn.addEventListener('click', () => {

    const testEl = document.getElementById('doctor');

    testEl.style.display = 'block';

});

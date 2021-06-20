$('.invoice button').click(function (e) {
    if (!$(this).parent().parent().parent().find('input[type="file"]').val()) {
        $('.alert-box').empty();
        $(this).parent().parent().parent().find('.alert-box').append('<div class="alert alert-danger">Zəhmət olmasa qəbzin şəklini daxil edin</div>')
        e.preventDefault();
    } else {
        $(this).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Gözləyin...');
        $(this).prop('disabled', true);
        $(this).parent().parent().parent().submit();
    }
})
$('#first_1').click(function () {
    $('#first-box_1').fadeToggle()
});
$('#first_2').click(function () {
    $('#first-box_2').fadeToggle()
});
$('#first_3').click(function () {
    $('#first-box_3').fadeToggle()
});
$('#first_4').click(function () {
    $('#first-box_4').fadeToggle()
});
$('#first_5').click(function () {
    $('#first-box_5').fadeToggle()
});
$('#first_6').click(function () {
    $('#first-box_6').fadeToggle()
});
$('#first_7').click(function () {
    $('#first-box_7').fadeToggle()
});

function myFunction() {
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    ul = document.getElementById("myUL");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1 ) {
            li[i].style.display = "";
        }
        else {
            li[i].style.display = "none";
        }
    }
    if (input.value().length() == 0){
        alert();
    }
}
$(document).ready(function () {
    update_amounts();
    update_amounts2();
    $('.count').on('keyup keypress blur change', function (e) {
        update_amounts();
    });
    $('.count2').on('change', function (e) {
        update_amounts2();
    });
});

function update_amounts() {
    var sum = 0.0;
    $('#mytable > tbody > tr').each(function () {
        var price = $(this).find('#price').text();
        var count = $(this).find('#count').val();
        var amount = (price * count)
        sum += amount;
        $(this).find('#amount').val('' + amount);
    })
    $('#total').text(sum);
}
function update_amounts2() {
    var sum2 = 0.0;
    $('.modal').each(function () {
        var price2 = $(this).find('.price2').val();
        var price3 = parseInt(price2);
        var count2 = $(this).find('.count2').val();
        var count3 = parseInt(count2);
        var amount2 = (price3 * count3)
        console.log(amount2);
        $(this).find('.amount2').val('' + amount2);
    })
}



$(document).ready(function () {

    // declare row for item 
    
    var list = [];

    ////////   Add row to item table      /////////
    $("#btnAdd").on('click', function () {
        var row = {
            Id: 0,
            ItemId: 0,
            ItemName: '',
            UnitId: 0,
            UnitName: '',
            Price: 0,
            Qty: 0,
            ItemTotal: 0,
            ItemDiscount: 0,
            ItemNet: 0
        };
        var isproceed = true;

        // validation
        row.ItemId = $('#ddlItem').val();
        if (row.ItemId != '' && row.ItemId != null) {
            $('#ddlItem').css('border-color', '#ccc');
            row.ItemName = $("#ddlItem option:selected").text();
        } else {
            $('#ddlItem').css('border-color', 'red');
            isproceed = false;
        }



        row.UnitId = $('#ddlUnit').val();
        if (row.UnitId != '' && row.UnitId != null) {
            $('#ddlUnit').css('border-color', '#ccc');
            row.UnitName = $("#ddlUnit option:selected").text();
        } else {
            $('#ddlUnit').css('border-color', 'red');
            isproceed = false;
        }

        row.Price = $('#Price').val();
        if (row.Price != '' && row.Price != null) {
            $('#Price').css('border-color', '#ccc');
        } else {
            $('#Price').css('border-color', 'red');
            isproceed = false;
        }

        row.Qty = $('#Quantity').val();
        if (row.Qty != '' && row.Qty != null) {
            $('#Quantity').css('border-color', '#ccc');
        } else {
            $('#Quantity').css('border-color', 'red');
            isproceed = false;
        }

        row.Id++;

        if (isproceed) {



            if (!isNaN(row.Qty) && row.Qty.length !== 0) {
                row.ItemTotal = parseFloat(row.Qty) * parseFloat(row.Price);
                $("#ItemTotal").val(row.ItemTotal);
            }


            row.ItemDiscount = $("#ItemDiscount").val();
            if (!isNaN(row.ItemDiscount) && row.ItemDiscount.length !== 0) {
                debugger;
                if (parseFloat(row.ItemDiscount) <= 100) {
                    var net = CalculateDiscount(row.ItemDiscount, row.ItemTotal)
                    //var dicount = (parseFloat(row.ItemTotal) * parseFloat(row.ItemDiscount)) / 100;
                    row.ItemNet = parseFloat(net);
                }
                else {

                    row.ItemDiscount = '';
                    row.ItemNet = row.ItemTotal;
                }

            }
            else {
                row.ItemNet = row.ItemTotal;
            }

            //var r=CalculateDiscount(row.ItemDiscount, row.ItemTotal);
            debugger;
            var rowTable = `<tr>
                    <td class="ItemName">${row.ItemName}</td>
                    <td class="UnitName">${row.UnitName}</td>
                    <td class="Price">${row.Price}</td>
                    <td class="Qty">${row.Qty}</td>
                    <td class="ItemTotal">${row.ItemTotal}</td>
                    <td class="ItemDiscount">${row.ItemDiscount}</td>
                    <td class="ItemNet">${row.ItemNet}</td>
                </tr>`;

            $('#Items').append(rowTable);
            debugger;
            list.push(row);
            calculateSum();
            resetItem();
        }
        else {
            resetItem();
        }
    });
    function resetItem() {
        $('#ddlUnit').val('');
        $('#ddlItem').val('');
        $('#Price').val('');
        $('#Quantity').val('');
        $('#ItemDiscount').val('');
       
    }
    function calculateSum() {
        var sum = 0;
        // iterate through each td based on class and add the values
        $(".ItemNet").each(function () {

            var value = $(this).text();
            var tax = $('#Tax').val();
            // add only if the value is number
            if (!isNaN(value) && value.length !== 0) {
                sum += parseFloat(value);
                if (isNaN(tax) || tax.length == 0) {
                   
                    $('#Net').val(sum);
                }
            }
        });

        $('#TotalAmount').val(sum);
    };

    $('.ItemNet').each(function () {
        calculateSum();
    });
    ///////////        Calculate Discount                        ///////////////

    function CalculateDiscount(dic, total) {
        var keyValue = "";
        var myformdata = new FormData();
        myformdata.append('discount', dic);
        myformdata.append('total', total);
        $.ajax({
            url: 'CalculateNetAfterDiscount',
            async: false,
            type: 'POST',
            data: myformdata,
            dataType: 'json',
            cache: false,

            processData: false, // Don't process the files
            contentType: false, // Set content type to false as jQuery will tell the server its a query string request
            success: function (data) {
                console.log(data);
                debugger;
                keyValue = data;

            },
            error: function () {
                debugger;
            }
        });
        return keyValue;
    }
    ////////////////////////////////////////////////////////////////////////////////
    ///////////        Calculate Tax                        ///////////////

    function CalculateNetAfterTax(tax, total) {
        var keyValue = "";
        var myformdata = new FormData();
        myformdata.append('tax', tax);
        myformdata.append('total', total);
        $.ajax({
            url: 'CalculateNetAfterTax',
            async: false,
            type: 'POST',
            data: myformdata,
            dataType: 'json',
            cache: false,

            processData: false, // Don't process the files
            contentType: false, // Set content type to false as jQuery will tell the server its a query string request
            success: function (data) {
                console.log(data);
                debugger;
                keyValue = data;

            },
            error: function () {
                debugger;
            }
        });
        return keyValue;
    }
    ////////////////////////////////////////////////////////////////////////////////
    //////  create event on tax input if has changed to calc total/////
    const input = document.querySelector("#Tax");
    input.addEventListener("change", keydownFunction);
    function keydownFunction(e) {
        var totalAmount = $('#TotalAmount').val();
        if (!isNaN(totalAmount) && totalAmount.length !== 0) {
            var total = parseFloat(totalAmount);
            debugger;
            var b = $('#Tax').val();
            if (!isNaN(b) && b.length !== 0) {
                var net = CalculateNetAfterTax(b, totalAmount)
                //var tax = parseFloat(b) / 100;
                //var net = (total + (tax * total));
                $('#Net').val(net);

            }
            else {
                $('#Net').val(totalAmount);
            }
           
        }

    }
    ////////////////////////////////////////////////////////////////////////

    $("#btnSubmit").click(function () {

        var myformdata = new FormData();
        myformdata.append('itemList', JSON.stringify(list));
        myformdata.append('serialNo', $('#SerialNo').val());
        myformdata.append('invoiceDate', $('#InvoiceDate').val());
        myformdata.append('storeId', $('#Store').val());
        myformdata.append('totalAmount', $('#TotalAmount').val());
        myformdata.append('tax', $('#Tax').val());
        myformdata.append('net', $('#Net').val());

        $.ajax({
            url: 'Save',
            type: 'POST',
            data: myformdata,
            cache: false,
            async: false,
            dataType: 'json',
            processData: false, // Don't process the files
            contentType: false, // Set content type to false as jQuery will tell the server its a query string request
            success: function (data) {
                var result = parseInt(data);
                switch (result) {
                    case 1:

                        toastr.success("تم الحفظ بنجاح");


                        break;
                    case 0:

                        toastr.error("");
                        break;
                }
            },

            error: function (e) {
                toastr.error(e);
            }
        });
    });
    ///////////////////////



    //////////////////////
});





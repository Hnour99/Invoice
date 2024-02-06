$(document).ready(function () {
  
    // declare row for item 
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
    var list = [];

    ////////   Add row to item table      /////////
    $("#btnAdd").on('click', function () {
        row.Id++;
        
        row.ItemId = $('#ddlItem').val();
        row.ItemName = $("#ddlItem option:selected").text();
        row.UnitId = $('#ddlUnit').val();
        row.UnitName = $("#ddlUnit option:selected").text();
        row.Price = $("#Price").val();
        row.Qty = $("#Quantity").val();
        if (!isNaN(row.Qty) && row.Qty.length !== 0) {
            row.ItemTotal = parseFloat(row.Qty) * parseFloat(row.Price);
            $("#ItemTotal").val(row.ItemTotal);
        }
        
        row.ItemDiscount = $("#ItemDiscount").val();
        if (!isNaN(row.ItemDiscount) && row.ItemDiscount.length !== 0) {
            var dicount = (parseFloat(row.ItemTotal) * parseFloat(row.ItemDiscount)) / 100;
            row.ItemNet = parseFloat(row.ItemTotal) - dicount;
        }
       
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
    });

    function calculateSum() {
        var sum = 0;
        // iterate through each td based on class and add the values
        $(".ItemNet").each(function () {

            var value = $(this).text();
            // add only if the value is number
            if (!isNaN(value) && value.length !== 0) {
                sum += parseFloat(value);
            }
        });

        $('#TotalAmount').val(sum);       
    };

    $('.ItemNet').each(function () {
        calculateSum();
    });

    //////  create event on tax input if has changed to calc total/////
    const input = document.querySelector("#Tax"); 
    input.addEventListener("change", keydownFunction);
    function keydownFunction(e) {
        var totalAmount = $('#TotalAmount').val();
        if (!isNaN(totalAmount) && totalAmount.length !== 0) {
            var total = parseFloat(totalAmount);

            var b = $('#Tax').val();
            var tax = parseFloat(b)/100;
            var net = (total + (tax * total));
            $('#Net').val(net);
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
            url: 'InvoiceOrder/Save',
            type: 'POST',
            data: myformdata,
            cache: false,
            dataType: 'json',
            processData: false, // Don't process the files
            contentType: false, // Set content type to false as jQuery will tell the server its a query string request
            success: function (data) {
                console.log(data);
             
                toastr.success("تم الحفظ بنجاح");
            },
            error: function () {
                debugger;
            }
        });
    });
    ///////////////////////
   


    //////////////////////
});





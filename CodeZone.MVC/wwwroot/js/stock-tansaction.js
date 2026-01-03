$(function () {

    var $warehouseSelect = $("#warehouseSelect");
    var $productSelect = $("#productSelect");
    var $stockDisplay = $("#currentStockDisplay");
    var $quantityInput = $("#quantityInput");
    var $increaseBtn = $("#increaseBtn");
    var $decreaseBtn = $("#decreaseBtn");

    if ($warehouseSelect.length === 0 || $productSelect.length === 0)
        return;

    // + button
    $increaseBtn.on("click", function () {
        var currentValue = parseInt($quantityInput.val()) || 0;
        $quantityInput.val(currentValue + 1);
    });

    // - button
    $decreaseBtn.on("click", function () {
        var currentValue = parseInt($quantityInput.val()) || 0;
        $quantityInput.val(currentValue - 1);
    });

    function loadCurrentStock() {
        var warehouseId = $warehouseSelect.val();
        var productId = $productSelect.val();

        if (!warehouseId || !productId) {
            $stockDisplay.text(
                "Select a warehouse and product to see current stock"
            );
            return;
        }

        $.ajax({
            url: "/StockTransaction/GetCurrentStock",
            type: "GET",
            data: {
                warehouseId: warehouseId,
                productId: productId
            },
            success: function (stock) {
                $stockDisplay.html(
                    "Current Stock: <strong>" + stock + "</strong>"
                );
            },
            error: function () {
                $stockDisplay.text("Error loading stock");
            }
        });
    }

    $warehouseSelect.on("change", loadCurrentStock);
    $productSelect.on("change", loadCurrentStock);

    // For Edit View (load on page open)
    loadCurrentStock();
});

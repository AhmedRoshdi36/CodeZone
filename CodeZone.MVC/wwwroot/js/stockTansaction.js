document.addEventListener("DOMContentLoaded", function () {

    const warehouseSelect = document.getElementById("warehouseSelect");
    const productSelect = document.getElementById("productSelect");
    const stockDisplay = document.getElementById("currentStockDisplay");
    const quantityInput = document.getElementById("quantityInput");
    const increaseBtn = document.getElementById("increaseBtn");
    const decreaseBtn = document.getElementById("decreaseBtn");

    if (!warehouseSelect || !productSelect) return;

    increaseBtn?.addEventListener("click", () => {
        quantityInput.value = (parseInt(quantityInput.value) || 0) + 1;
    });

    decreaseBtn?.addEventListener("click", () => {
        quantityInput.value = (parseInt(quantityInput.value) || 0) - 1;
    });

    async function loadCurrentStock() {
        const warehouseId = warehouseSelect.value;
        const productId = productSelect.value;

        if (!warehouseId || !productId) {
            stockDisplay.innerText =
                "Select a warehouse and product to see current stock";
            return;
        }

        try {
            const response = await fetch(
                `/StockTransaction/GetCurrentStock?warehouseId=${warehouseId}&productId=${productId}`
            );

            const stock = await response.json();
            stockDisplay.innerHTML =
                `Current Stock: <strong>${stock}</strong>`;
        }
        catch {
            stockDisplay.innerText = "Error loading stock";
        }
    }

    warehouseSelect.addEventListener("change", loadCurrentStock);
    productSelect.addEventListener("change", loadCurrentStock);

    loadCurrentStock();
});

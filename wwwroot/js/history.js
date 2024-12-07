$(document).ready(function () {

    const usernameHistory = localStorage.getItem("username")
    const passwordHistory = localStorage.getItem("password")

    if(!usernameHistory || !passwordHistory) {
        window.location.href = "/login"
    }

    $.when(
        $.ajax({ url: `http://localhost:5115/api/ltpayment/view`, method: 'GET' }),
        $.ajax({ url: `http://localhost:5115/api/trrental/view?customer_id=${localStorage.getItem("id")}`, method: 'GET' }),
        $.ajax({ url: 'http://localhost:5115/api/mscar/view', method: 'GET' })
    ).done(function (paymentData, rentalData, carData) {
        var paymentsData = paymentData[0].data;
        var rentalsData = rentalData[0].data;
        var carsData = carData[0].data;

        paymentsData.forEach(function (payment) {
            var rentalIndex = rentalsData.find(i => i.rental_id === payment.rental_id);
            var carIndex = carsData.find(i => i.car_id === rentalIndex.car_id)

            var statusClass = rentalsData.payment_status === "true" ? 'status-paid' : 'status-unpaid';
            var statusText = rentalsData.payment_status === "true" ? 'Sudah Dibayar' : 'Belum Dibayar';

            $('.table tbody').append(`
                <tr>
                    <td>${rentalIndex.rental_Date} - ${rentalIndex.return_Date}</td>
                    <td>${carIndex.name}</td>
                    <td>Rp. ${carIndex.price_per_day.toLocaleString()}</td>
                    <td>${Math.floor(rentalIndex.total_price/carIndex.price_per_day)}</td>
                    <td>Rp. ${rentalIndex.total_price.toLocaleString()}</td>
                    <td class="${statusClass}">${statusText}</td>
                </tr>
            `);
        });
    }).fail(function (error) {
        console.log("Error fetching data:", error);
    });
})
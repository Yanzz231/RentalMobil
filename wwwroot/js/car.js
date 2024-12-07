$(document).ready(function () {
    // CAR DETAIL
    let images = [];
    let currentIndex = 0;
    let carPrize = 0;
    let NameCar;

    $.ajax({
        url: `http://localhost:5115/api/mscarimages/view?car_id=${carId}`,
        method: "GET",
        dataType: "json",
        success: function (response) {
            if (response.message === "Menampilkan Data") {
                images = response.data.map(item => item.image_link);
                if (images.length > 0) {
                    $("#carouselImage").attr("src", images[currentIndex]);
                }
            }
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });

    $.ajax({
        url: `http://localhost:5115/api/mscar/view?id=${carId}`,
        method: "GET",
        dataType: "json",
        success: function (response) {
            if (response.message === "Menampilkan Data" && response.data) {
                const carData = response.data;
                NameCar = carData?.name
                carPrize = carData?.price_per_day

                $(".car-info-card table").html(`
                        <tr>
                            <td class="car-label">Tipe Mobil</td>
                            <td>${carData.model}</td>
                        </tr>
                        <tr>
                            <td class="car-label">Nama Mobil</td>
                            <td>${carData.name} ${carData.year}</td>
                        </tr>
                        <tr>
                            <td class="car-label">Transmisi</td>
                            <td>${carData.transmission}</td>
                        </tr>
                        <tr>
                            <td class="car-label">Jumlah Penumpang</td>
                            <td>${carData.number_of_car_seats} Penumpang</td>
                        </tr>
                        <tr>
                            <td class="car-label">Plat Nomor</td>
                            <td>${carData.license_Plate}</td>
                        </tr>
                        <tr>
                            <td class="car-label">Harga Sewa</td>
                            <td>Rp. ${carData.price_per_day.toLocaleString()} / hari</td>
                        </tr>
                    `);

                if (carData.status === "true") {
                    $(".car-rent-button").prop("disabled", false).text("Sewa");
                } else {
                    $(".car-rent-button").prop("disabled", true).text("Tidak Tersedia");
                }
            } else {
                console.error("Data not found or invalid format");
            }
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });


    // CAROUSEL 
    $(".car-next").click(function () {
        if (images.length > 0) {
            currentIndex = (currentIndex + 1) % images.length;
            $("#carouselImage").attr("src", images[currentIndex]);
        }
    });

    $(".car-prev").click(function () {
        if (images.length > 0) {
            currentIndex = (currentIndex - 1 + images.length) % images.length;
            $("#carouselImage").attr("src", images[currentIndex]);
        }
    });


    // RENTAL SUBMIT
    $('#button_rent').click(function () {
        const rentalDateStr = $('#rental_date_input').val();
        const returnDateStr = $('#return_date_input').val();
        const optionMethod = $('#method_payment').val();

        if (!rentalDateStr || !returnDateStr || !optionMethod) return alert("Input tidak boleh kosong")

        const rentalDate = new Date(rentalDateStr);
        const returnDate = new Date(returnDateStr);

        const timeDiff = returnDate - rentalDate;
        const dayDiff = Math.floor(timeDiff / (1000 * 60 * 60 * 24));

        if (dayDiff < 1) {
            return alert("Tolong Isi return date tidak boleh sebelum rental date");
        }

        $.ajax({
            url: 'http://localhost:5115/api/trrental/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Rental_Date: rentalDateStr,
                Return_Date: returnDateStr,
                Total_Price: dayDiff * carPrize,
                Customer_id: parseInt(localStorage.getItem("id")),
                Car_id: carId,
                Payment_status: "false"
            }),
            success: function (response) {
                $.ajax({
                    url: 'http://localhost:5115/api/ltpayment/create',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify({
                        Amout: 0,
                        Payment_method: optionMethod,
                        Rental_id: response.data.rental_id
                    }),
                    success: function (response) {
                        return alert(`Berhasil Sewa Mobil ${NameCar} seharga Rp. ${(dayDiff * carPrize).toLocaleString()} Dengan metode pembayaran ${optionMethod}`)
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(jqXHR)
                        alert('Terjadi Kesalahan');
                        return;
                    }
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR)
                alert('Terjadi Kesalahan');
                return;
            }
        });

    });
});
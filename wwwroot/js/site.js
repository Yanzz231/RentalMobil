$(document).ready(function () {
    // REGISTER PAGE
    $('#register-submit').click(function (e) {
        e.preventDefault();

        const Name = $('#Name').val();
        const Email = $('#Email').val();
        const Password = $('#Password').val();
        const Re_password = $('#Re-password').val();
        const Phone = $('#Phone').val();
        const Address = $('#Address').val();

        
        if (Password !== Re_password) {
            alert('Password dan repassword tidak sama');
            return;
        }

        if(Password.length >= 8 || Re_password.length >= 8) {
            alert('Input Password harus lebih 8 karakter')
        }

        if (Name === '' || Password === '' || Re_password === '' || Email === '' || Phone === '' || Address === '') {
            alert('Username dan password tidak boleh kosong!');
            return;
        }

        $.ajax({
            url: 'http://localhost:5115/api/mscustomer/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                Email: Email,
                Password: Password,
                Name: Name,
                Phone_number: Phone,
                Address: Address,
                Driver_license_number: "B"

            }),
            success: function (response) {
                window.location.href = "/login"
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR)
                alert('Terjadi Kesalahan');
                return;
            }
        });
    });



    // LOGIN PAGE
    $('#login-submit').click(function (e) {
        e.preventDefault();

        const Name = $('#Name').val();
        const Password = $('#Password').val();

        if (Name === '' || Password === '') {
            alert('Username dan password tidak boleh kosong!');
            return;
        }

        $.ajax({
            url: 'http://localhost:5115/api/mscustomer/view',
            type: 'GET',
            data: {
                username: Name,
                password: Password
            },
            success: function (response) {
                console.log(response)
                localStorage.setItem("username", Name)
                localStorage.setItem("password", Password)
                localStorage.setItem("id", response.data.customer_id)
                window.location.href = "/"
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Pastikan username dan password benar');
                return;
            }
        });
    });

    // NAVBAR COMPONENT
    const username = localStorage.getItem('username'); // NAMA AKU
    if (username) {
        $('#login-btn').hide();
        $('#register-btn').hide();

        $('#account-name').text(`Hai, ${username}`);
    } else {
        $('#button-logout').hide();
    }

    // LOGOUT BUTTON
    $('#button-logout').click(function (e) {
        e.preventDefault();
        localStorage.removeItem("username")
        localStorage.removeItem("password")
        localStorage.removeItem("id")
        window.location.href = "/login"
    })

    const toggleMenu = document.getElementById("hamburgerIcon"); // HUMBERGER
    const untoggleMenu = document.getElementById("closeBtn");
    const sidebar = document.getElementById("navSlide");

    toggleMenu.addEventListener("click", function () {
        sidebar.classList.remove("hide");
    });

    untoggleMenu.addEventListener("click", function () {
        sidebar.classList.toggle("hide");
    });

    // INDEX PAGE
    for (let year = 2024; year >= 1900; year--) {
        $('#yearSelect').append($('<option></option>').attr('value', year).text(year));
    }


    // DATA CONTAINER
    let sortBy = 0;
    let total = 0;

    function loadAllCarData(sortOrder = '0') {
        $.ajax({
            url: `http://localhost:5115/api/mscar/view`,
            type: 'GET',
            success: function (response) {
                if (response.message === "Menampilkan Semua Data") {
                    sortCarsData(sortOrder);
                    total = response.data.length
                    updatePagination(response.data.length, 8);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error fetching car data: ", error);
                $('#car-container').append('<p class="text-white">Error fetching data</p>');
            }
        });
    }

    function sortCarsData(sortOrder) {
        sortBy = sortOrder
        loadPageData(1)
    }
    function loadPageData(page, data = null) {
        const itemsPerPage = 8;
        console.log(`http://localhost:5115/api/mscar/view?${sortBy !== '0' ? `sortBy=${sortBy}&page=0` : `${data !== null ? `` : `page=${page}&pageSize=${itemsPerPage}`}&pageSize=${itemsPerPage}${data !== null ? `${data.year === "" ? "": `&year=${data.year}`}${data.ReturnDate === "" ? "": `&ReturnDate=${data.ReturnDate}`}${data.PickDate === "" ? "": `&PickDate=${data.PickDate}`}`: ""}`}`)
        $.ajax({
            url: `http://localhost:5115/api/mscar/view?${sortBy !== '0' ? `sortBy=${sortBy}&page=0` : `${data !== null ? `` : `page=${page}&pageSize=${itemsPerPage}`}&pageSize=${itemsPerPage}${data !== null ? `${data.year === "" ? "": `&year=${data.year}`}${data.ReturnDate === "" ? "": `&ReturnDate=${data.ReturnDate}`}${data.PickDate === "" ? "": `&PickDate=${data.PickDate}`}`: ""}`}`,
            type: 'GET',
            success: function (response) {
                    $('#car-container').empty();
                    response.data.forEach(function (car) {
                        $.ajax({
                            url: `http://localhost:5115/api/mscarimages/view?car_id=${car.car_id}`,
                            type: 'GET',
                            success: function (imageResponse) {
                                let imageLink = "path_to_placeholder_image.jpg";
                                if (imageResponse.data && imageResponse.data.length > 0) {
                                    imageLink = imageResponse.data[0].image_link;
                                }
                                $('#car-container').append(`
                                <div class="border-radius-m bg-secondary w-70 h-75 justify-center flex flex-col item-center">
                                    <img class="w-60 mt-4 mb-3 h-30" src="${imageLink}" alt="${car.name}">
                                    <div class="mb-4 text-center justify-between flex flex-col">
                                        <h1 class="text-white text-xl mb-2">${car.name}</h1>
                                        <p class="text-white text-xs mb-3">Harga: Rp ${car.price_per_day.toLocaleString()} / Hari</p>
                                        <button class="bg-blue-light text-white p-2 border-radius-s w-full button"><a href="/car/${car.car_id}" class="button text-href text-white">Sewa Sekarang</a></button>
                                    </div>
                                </div>
                            `);
                            },
                            error: function (xhr, status, error) {
                                console.error("Error fetching image data: ", error);
                            }
                        });
                    });
                    updatePagination(response.totalItems, itemsPerPage);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching car data: ", error);
                $('#car-container').append('<p class="text-white">Error fetching data</p>');
            }
        });
    }

    function updatePagination(totalItems, itemsPerPage) {
        const totalPages = Math.ceil(totalItems / itemsPerPage);
        const paginationContainer = $('#pagination-container');
        paginationContainer.empty();
        for (let i = 1; i <= totalPages; i++) {
            paginationContainer.append(`
            <button class="pagination-button bg-blue-light button p-3 border-radius-s text-white" data-page=${i}>${i}</button>
        `);
        }

        $('.pagination-button').off('click').on('click', function () {
            const page = $(this).data('page');
            sortBy = '0'
            loadPageData(page);
        });
    }

    $('#sort-select').on('change', function () {
        const selectedSort = $(this).val();
        sortCarsData(selectedSort);
    });

    // FILTER
    $('#submit-filter').click(function (e) {
        const pickupDate = $('#pickupDate').val();
        const returnDate = $('#returnDate').val();
        const selectedYear = $('#yearSelect').val();
        sortBy = '0'
        loadPageData(1, {year: selectedYear, ReturnDate: returnDate, PickDate: pickupDate});
    })

    loadAllCarData();
});
-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 28, 2024 at 04:46 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `itdiv`
--

CREATE Database `itdiv`;
USE `itdiv`;

-- --------------------------------------------------------

--
-- Table structure for table `ltpayment`
--

CREATE TABLE `ltpayment` (
  `Payment_id` int(11) NOT NULL,
  `Payment_date` varchar(255) NOT NULL,
  `Amount` int(11) NOT NULL,
  `Payment_method` varchar(255) NOT NULL,
  `Rental_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ltpayment`
--

INSERT INTO `ltpayment` (`Payment_id`, `Payment_date`, `Amount`, `Payment_method`, `Rental_id`) VALUES
(18, '2024-10-28 20:58:37', 0, 'Debit Card', 4),
(19, '2024-10-28 21:00:55', 0, 'Debit Card', 5),
(20, '2024-10-28 21:04:55', 0, 'Cash', 6);

-- --------------------------------------------------------

--
-- Table structure for table `mscar`
--

CREATE TABLE `mscar` (
  `Car_id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Model` varchar(255) NOT NULL,
  `Year` int(11) NOT NULL,
  `License_Plate` varchar(255) NOT NULL,
  `Number_of_car_seats` int(11) NOT NULL,
  `Transmission` varchar(255) NOT NULL,
  `Price_per_day` int(11) NOT NULL,
  `Status` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mscar`
--

INSERT INTO `mscar` (`Car_id`, `Name`, `Model`, `Year`, `License_Plate`, `Number_of_car_seats`, `Transmission`, `Price_per_day`, `Status`) VALUES
(1, 'Nissan GTR', 'R35', 2024, 'A 1234 AB', 2, 'Automatic', 5000000, 'true'),
(2, 'Ferrari 488', 'GTB', 2023, 'R 6789 IJ', 2, 'Automatic', 18000000, 'true'),
(3, 'Mitsubishi Lancer', 'Evolution', 2024, 'Q 2345 GH', 5, 'Manual', 3700000, 'true'),
(4, 'Tesla Model S', 'Plaid', 2022, 'P 9101 EF', 5, 'Automatic', 13000000, 'true'),
(5, 'Jaguar F-Type', 'P300', 2023, 'O 6789 CD', 2, 'Automatic', 10500000, 'true'),
(6, 'Lexus LC', '500', 2024, 'N 2345 AB', 4, 'Automatic', 11000000, 'true'),
(7, 'Nissan 370Z', 'Nismo', 2022, 'M 9101 YZ', 2, 'Manual', 3800000, 'true'),
(8, 'Audi R8', 'V10', 2023, 'L 5678 WX', 2, 'Automatic', 15000000, 'true'),
(9, 'Subaru WRX', 'STI', 2024, 'K 1234 UV', 5, 'Manual', 4200000, 'true'),
(10, 'Mercedes-Benz C-Class', 'AMG C 63', 2022, 'J 6789 ST', 5, 'Automatic', 8500000, 'true');

-- --------------------------------------------------------

--
-- Table structure for table `mscarimages`
--

CREATE TABLE `mscarimages` (
  `Image_Car_Id` int(11) NOT NULL,
  `Car_id` int(11) NOT NULL,
  `Image_link` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mscarimages`
--

INSERT INTO `mscarimages` (`Image_Car_Id`, `Car_id`, `Image_link`) VALUES
(1, 1, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFfNXNjHjJtBlVwXx_NQ-RPCqJKj_xLIgZjg&s'),
(2, 1, 'https://storage.googleapis.com/prod-nissan-indonesia/static-assets/brand/18tdigb-helios307c.jpg.ximg.l_6_m.smart.jpg'),
(3, 1, 'https://asset.kompas.com/crops/K97bPa1iOPmgaacOj1X2HR0v29I=/84x0:948x576/1200x800/data/photo/2023/01/13/63c0d90a793d6.jpg'),
(4, 2, 'https://imgcdnblog.carvaganza.com/wp-content/uploads/2016/07/New-Toyota-Supra-Dikabarkan-Gunakan-mesin-V6-Twin-Turbo-Lexus.jpg'),
(5, 2, 'https://planetban.com/media/blog/post/image_small/Toyota_Supra_Monrepos_2019_IMG_1898.jpeg'),
(6, 2, 'https://cdn.antaranews.com/cache/1200x800/2019/07/09/Toyota-New-Supra.jpg'),
(7, 3, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7j70R_bhjA3uj5GDDzvRyg4wEh8g-zJHktQ&s'),
(8, 3, 'https://www.carscoops.com/wp-content/uploads/2024/02/2024-Mitsubishi-Lancer-EVO-11-_1-copy-scaled.jpg'),
(10, 4, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFrA6wMymvCuo48jOfP3U_7n9EACwDUdVmSA&s'),
(11, 4, 'https://awsimages.detik.net.id/community/media/visual/2022/08/12/mazda-mx-5_169.jpeg?w=650'),
(12, 5, 'https://imgcdn.oto.com/large/gallery/exterior/23/1550/mazda-mx-5-rf-front-angle-low-view-230102.jpg'),
(13, 5, 'https://cdn.motor1.com/images/mgl/MNBXn/s1/2022-ford-mustang-shelby-gt500-heritage-edition-with-original-close-crop.jpg'),
(14, 5, 'https://imgx.gridoto.com/crop/156x108:1777x1007/700x465/photo/2019/08/15/3711207912.jpg'),
(15, 5, 'https://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/2018_Ford_Mustang_GT_5.0.jpg/1200px-2018_Ford_Mustang_GT_5.0.jpg'),
(16, 6, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWfDP9qYBwnTV1bp_9mF83ZK4juPn__VUxlw&s'),
(17, 6, 'https://cdn.motor1.com/images/mgl/1ZQwYp/s3/hennessey-chevrolet-camaro-zl1-exorcist-final-edition.jpg'),
(18, 7, 'https://imgcdn.oto.com/medium/gallery/exterior/31/265/porsche-911-64913.jpg'),
(19, 7, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQgO1ftAr2NWDYIsylUHpyS6ZdguXBlZoWgtQ&s'),
(20, 7, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRb_zd0WOVVBP9Bo2Lrqq6HmrLrdI55IR7GhA&s'),
(21, 8, 'https://www.bmw.co.id/content/dam/bmw/common/all-models/m-series/m3-sedan/2023/highlights/bmw-3-series-cs-m-automobiles-sp-desktop.jpg'),
(22, 8, 'https://www.bmw.co.id/content/dam/bmw/common/all-models/m-series/m3-sedan/2023/highlights/bmw-3-series-cs-m-automobiles-gallery-impressions-m3-competition-03_1920.jpg.asset.1669650891815.jpg'),
(23, 9, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQX_r81snW4OeBYwC8XBypWsptLIsUBrPi63g&s'),
(24, 9, 'https://imgcdn.oto.com/large/gallery/color/25/209/mercedes-benz-c-class-color-738375.jpg'),
(25, 10, 'https://inging.s3.ap-southeast-1.amazonaws.com/website/subaru/wrx/files/aab1838a6c4eb4b903390d715cf3d3.jpg'),
(26, 10, 'https://inging.s3-ap-southeast-1.amazonaws.com/website/pages/subaru_overview_8_mobile.jpeg');

-- --------------------------------------------------------

--
-- Table structure for table `mscustomer`
--

CREATE TABLE `mscustomer` (
  `Customer_id` int(11) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Phone_number` varchar(255) NOT NULL,
  `Address` varchar(255) NOT NULL,
  `Driver_license_number` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mscustomer`
--

INSERT INTO `mscustomer` (`Customer_id`, `Email`, `Password`, `Name`, `Phone_number`, `Address`, `Driver_license_number`) VALUES
(1, 'example@gmail.com', '12345678', 'Customer', '08231231231', 'Jl.Alsut', 'B');

-- --------------------------------------------------------

--
-- Table structure for table `msemployee`
--

CREATE TABLE `msemployee` (
  `Employee_id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Position` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Phone_number` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `trmaintenance`
--

CREATE TABLE `trmaintenance` (
  `Maintenance_id` int(11) NOT NULL,
  `Maintenance_date` varchar(255) NOT NULL,
  `Decription` varchar(255) NOT NULL,
  `Cost` int(11) NOT NULL,
  `Car_id` int(11) NOT NULL,
  `Employee_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `trrental`
--

CREATE TABLE `trrental` (
  `Rental_id` int(11) NOT NULL,
  `Rental_Date` varchar(255) NOT NULL,
  `Return_Date` varchar(255) NOT NULL,
  `Total_price` int(11) NOT NULL,
  `Payment_status` varchar(255) NOT NULL,
  `Customer_id` int(11) NOT NULL,
  `Car_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `trrental`
--

INSERT INTO `trrental` (`Rental_id`, `Rental_Date`, `Return_Date`, `Total_price`, `Payment_status`, `Customer_id`, `Car_id`) VALUES
(4, '2024-10-28', '2024-10-29', 5000000, 'false', 1, 1),
(5, '2024-10-28', '2024-10-29', 5000000, 'false', 1, 1),
(6, '2024-10-28', '2024-10-30', 10000000, 'false', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20241026062753_InitialCreate', '8.0.10'),
('20241026063939_InitialCreate', '8.0.10');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `ltpayment`
--
ALTER TABLE `ltpayment`
  ADD PRIMARY KEY (`Payment_id`);

--
-- Indexes for table `mscar`
--
ALTER TABLE `mscar`
  ADD PRIMARY KEY (`Car_id`);

--
-- Indexes for table `mscarimages`
--
ALTER TABLE `mscarimages`
  ADD PRIMARY KEY (`Image_Car_Id`);

--
-- Indexes for table `mscustomer`
--
ALTER TABLE `mscustomer`
  ADD PRIMARY KEY (`Customer_id`);

--
-- Indexes for table `msemployee`
--
ALTER TABLE `msemployee`
  ADD PRIMARY KEY (`Employee_id`);

--
-- Indexes for table `trmaintenance`
--
ALTER TABLE `trmaintenance`
  ADD PRIMARY KEY (`Maintenance_id`);

--
-- Indexes for table `trrental`
--
ALTER TABLE `trrental`
  ADD PRIMARY KEY (`Rental_id`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `ltpayment`
--
ALTER TABLE `ltpayment`
  MODIFY `Payment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `mscar`
--
ALTER TABLE `mscar`
  MODIFY `Car_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `mscarimages`
--
ALTER TABLE `mscarimages`
  MODIFY `Image_Car_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `mscustomer`
--
ALTER TABLE `mscustomer`
  MODIFY `Customer_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `msemployee`
--
ALTER TABLE `msemployee`
  MODIFY `Employee_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `trmaintenance`
--
ALTER TABLE `trmaintenance`
  MODIFY `Maintenance_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `trrental`
--
ALTER TABLE `trrental`
  MODIFY `Rental_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 27, 2019 at 08:34 AM
-- Server version: 10.1.26-MariaDB
-- PHP Version: 7.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tokobuku`
--

-- --------------------------------------------------------

--
-- Table structure for table `buku`
--

CREATE TABLE `buku` (
  `id` varchar(100) NOT NULL,
  `judul` varchar(100) NOT NULL,
  `penulis` varchar(100) NOT NULL,
  `harga` int(100) NOT NULL,
  `stock` int(100) NOT NULL,
  `tahun` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `buku`
--

INSERT INTO `buku` (`id`, `judul`, `penulis`, `harga`, `stock`, `tahun`) VALUES
('B001', 'Evolusi Rindu', 'Genta Kiswara', 50000, 20, 2015),
('B002', 'Dunia Shopie', 'Jostein Gaarder', 100000, 2, 2010);

-- --------------------------------------------------------

--
-- Table structure for table `detail_transaksi`
--

CREATE TABLE `detail_transaksi` (
  `transaksi_id` varchar(100) NOT NULL,
  `buku_id` varchar(100) NOT NULL,
  `judul_buku` varchar(100) NOT NULL,
  `harga_buku` int(100) NOT NULL,
  `quantity` int(100) NOT NULL,
  `subtotal` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `detail_transaksi`
--

INSERT INTO `detail_transaksi` (`transaksi_id`, `buku_id`, `judul_buku`, `harga_buku`, `quantity`, `subtotal`) VALUES
('T190621001', 'B001', 'Evolusi Rindu', 50000, 4, 200000),
('T190621002', 'B001', 'Evolusi Rindu', 50000, 14, 700000),
('T190621003', 'B001', 'Evolusi Rindu', 50000, 1, 50000),
('T190623004', 'B002', 'Dunia Shopie', 100000, 8, 800000);

-- --------------------------------------------------------

--
-- Table structure for table `penerbit`
--

CREATE TABLE `penerbit` (
  `id` varchar(100) NOT NULL,
  `nama` varchar(100) NOT NULL,
  `telp` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `penerbit`
--

INSERT INTO `penerbit` (`id`, `nama`, `telp`) VALUES
('P001', 'Gagas Media', 483959);

-- --------------------------------------------------------

--
-- Table structure for table `transaksi`
--

CREATE TABLE `transaksi` (
  `id` varchar(100) NOT NULL,
  `tanggal` date NOT NULL,
  `waktu` time NOT NULL,
  `item` int(100) NOT NULL,
  `total` int(100) NOT NULL,
  `bayar` int(100) NOT NULL,
  `kembali` int(100) NOT NULL,
  `penerbit_id` varchar(100) NOT NULL,
  `user_id` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `transaksi`
--

INSERT INTO `transaksi` (`id`, `tanggal`, `waktu`, `item`, `total`, `bayar`, `kembali`, `penerbit_id`, `user_id`) VALUES
('T190621001', '2019-06-21', '02:36:16', 4, 200000, 250000, 50000, 'P001', 'U001'),
('T190621002', '2019-06-21', '03:12:29', 14, 700000, 1000000, 300000, 'P001', 'U001'),
('T190621003', '2019-06-21', '03:13:26', 1, 50000, 100000, 50000, 'P001', 'U001'),
('T190623004', '2019-06-23', '09:35:33', 8, 800000, 1000000, 200000, 'P001', 'U001');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` varchar(100) NOT NULL,
  `nama` varchar(100) NOT NULL,
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `level` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `nama`, `username`, `password`, `level`) VALUES
('U001', 'faiz', 'faiz', 'faiz', 'user'),
('U002', 'izeah', 'izeah', 'izeah', 'user'),
('U003', 'aku', 'aku', 'aku', 'aku');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `buku`
--
ALTER TABLE `buku`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `penerbit`
--
ALTER TABLE `penerbit`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `transaksi`
--
ALTER TABLE `transaksi`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

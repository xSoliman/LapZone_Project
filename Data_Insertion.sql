-- Insert categories into the Category table
INSERT INTO Category (CategoryName, _Description, ImagePath)
VALUES 
('Laptops', 'Laptops and Notebooks', 'laptops_category.jpg'),
('Mice', 'Computer Mice and Pointing Devices', 'mice_category.jpg'),
('Keyboards', 'Computer Keyboards', 'keyboards_category.jpg'),
('Stands', 'Laptop Stands and Accessories', 'stands_category.jpg'),
('Headphones', 'Headphones and Audio Accessories', 'headphones_category.jpg');


-- Assuming you have a stored procedure or other mechanism to handle IFormFile
-- This example uses a parameter @FilePath to represent the file path in the database

DECLARE @FilePath NVARCHAR(MAX);
SET @FilePath = '/uploads/';

-- Insert 15 rows (for laptops and accessories)
INSERT INTO Product (ProductName, _Description, Price, CategoryId, StockQuantity, ImagePath)
VALUES 
('High-Performance Laptop', 'Powerful laptop with cutting-edge features', 1299.99, 1, 50, @FilePath + 'high_performance_laptop.jpg'),
('Gaming Laptop', 'Laptop optimized for gaming experience', 1599.99, 1, 40, @FilePath + 'gaming_laptop.jpg'),
('Business Laptop', 'Professional laptop for business use', 999.99, 1, 30, @FilePath + 'business_laptop.jpg'),

('Wireless Mouse', 'Ergonomic wireless mouse for improved productivity', 29.99, 2, 80, @FilePath + 'wireless_mouse.jpg'),
('Gaming Mouse', 'High-precision gaming mouse for gamers', 49.99, 2, 60, @FilePath + 'gaming_mouse.jpg'),

('Wireless Keyboard', 'Slim wireless keyboard for comfortable typing', 39.99, 3, 50, @FilePath + 'wireless_keyboard.jpg'),
('Mechanical Keyboard', 'Mechanical keyboard for gaming enthusiasts', 79.99, 3, 30, @FilePath + 'mechanical_keyboard.jpg'),
('Compact Keyboard', 'Compact keyboard for space-saving setups', 29.99, 3, 40, @FilePath + 'compact_keyboard.jpg'),

('Adjustable Laptop Stand', 'Height-adjustable laptop stand for improved ergonomics', 34.99, 4, 40, @FilePath + 'adjustable_laptop_stand.jpg'),
('Foldable Laptop Stand', 'Foldable laptop stand for portability', 19.99, 4, 60, @FilePath + 'foldable_laptop_stand.jpg'),

('Bluetooth Earbuds', 'Wireless Bluetooth earbuds for on-the-go audio', 59.99, 5, 60, @FilePath + 'bluetooth_earbuds.jpg'),
('Over-Ear Headphones', 'High-quality over-ear headphones for immersive sound', 99.99, 5, 30, @FilePath + 'over_ear_headphones.jpg'),
('Noise-Canceling Headphones', 'Noise-canceling headphones for a distraction-free experience', 129.99, 5, 20, @FilePath + 'noise_canceling_headphones.jpg'),
('Sports Earphones', 'Sweat-resistant sports earphones for active lifestyles', 49.99, 5, 50, @FilePath + 'sports_earphones.jpg'),
('Gaming Headset', 'Immersive gaming headset for gamers', 79.99, 5, 40, @FilePath + 'gaming_headset.jpg');

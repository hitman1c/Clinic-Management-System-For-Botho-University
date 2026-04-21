  <?php
  // Database credentials for XAMPP (default)
  $host = 'localhost';
  $username = 'root';  // Default XAMPP user
  $password = '';      // Default empty password
  $dbname = 'BothoUniversityClinic';  // Your target database

  // Create connection using MySQLi
  $conn = new mysqli($host, $username, $password);

  // Check connection
  if ($conn->connect_error) {
      die("Connection failed: " . $conn->connect_error);
  }
  echo "<strong>✓ Connected to MySQL server successfully!</strong><br>";

  // Create database if not exists
  $sql_create_db = "CREATE DATABASE IF NOT EXISTS $dbname";
  if ($conn->query($sql_create_db) === TRUE) {
      echo "<strong>✓ Database '$dbname' created or already exists.</strong><br>";
  } else {
      echo "Error creating database: " . $conn->error . "<br>";
  }

  // Select the database
  $conn->select_db($dbname);

  // Test by running a simple query (e.g., create a test table if needed)
  $sql_test = "CREATE TABLE IF NOT EXISTS test_table (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(50))";
  if ($conn->query($sql_test) === TRUE) {
      echo "<strong>✓ Test table created successfully in '$dbname'.</strong><br>";
  } else {
      echo "Error: " . $conn->error . "<br>";
  }

  // For importing your full SQL script: Read and execute file contents
  $script_path = 'C:/Users/SECHABA/Desktop/last me/Database/BothoUniversityClinic.sql';  // Use forward slashes
  if (file_exists($script_path)) {
      $sql_script = file_get_contents($script_path);
      if ($conn->multi_query($sql_script)) {
          echo "<strong>✓ SQL script imported successfully!</strong><br>";
          do { /* Consume results */ } while ($conn->next_result());
      } else {
          echo "Error importing script: " . $conn->error . "<br>";
      }
  } else {
      echo "<strong>⚠ Script file not found at: $script_path</strong><br>";
      echo "Place 'BothoUniversityClinic.sql' there or use manual import.<br>";
  }

  // Close connection
  $conn->close();
  echo "<strong>Connection closed. All tests passed!</strong>";
  ?>
  
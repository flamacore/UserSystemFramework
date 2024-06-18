<?php

namespace USF;

class USFUtilities
{
	function guidv4($data = null)
	{
		$data = $data ?? random_bytes(16);
		assert(strlen($data) == 16);
		$data[6] = chr(ord($data[6]) & 0x0f | 0x40);
		$data[8] = chr(ord($data[8]) & 0x3f | 0x80);
		return vsprintf('%s%s-%s-%s-%s-%s%s%s', str_split(bin2hex($data), 4));
	}

	function createSaltedPasswordHash($password, $salt = null)
	{
		if ($salt === null) {
			$salt = base64_encode(openssl_random_pseudo_bytes(32));
		}
		$saltedPassword = $password . $salt;
		$hashedPassword = hash('sha256', $saltedPassword);
		return array($hashedPassword, $salt);
	}

	function checkPassword($password, $hashedPassword, $salt)
	{
		list($expectedHashedPassword, $expectedSalt) = $this->createSaltedPasswordHash($password, $salt);
		return $hashedPassword === $expectedHashedPassword;
	}

	function IsNullOrEmptyString($str)
	{
		return ($str === null || trim($str) === '');
	}
	function returnHeaders(array $headers)
	{
		global $decodedUserData;
		global $usfEncrytpion;
		if ($decodedUserData->ID > -1) {
			header('RequestingUser: ' . $_POST["userData"] . '');
		}
		foreach ($headers as $key => $value) {
			header('' . $key . $value . '');
		}
	}
}
?>
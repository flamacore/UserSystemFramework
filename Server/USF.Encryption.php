<?php
namespace USF;

class USFEncryption
{
    function decrypt($string_to_decrypt)
    {
        $password = '3sc3RLrpd17';
        $method = 'aes-256-cbc';
        $key = substr(hash('sha256', $password, true), 0, 32);
        $iv = chr(0x0) . chr(0x0) . chr(0x43) . chr(0x24) . chr(0x15) . chr(0x14) . chr(0x0) . chr(0x0) . chr(0x48) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x19) . chr(0x0) . chr(0x17) . chr(0x0);
        return \openssl_decrypt(base64_decode($string_to_decrypt), $method, $key, OPENSSL_RAW_DATA, $iv);
    }
    //Encrypt string
    function encrypt($string_to_encrypt)
    {
        $password = '3sc3RLrpd17';
        $method = 'aes-256-cbc';
        $key = substr(hash('sha256', $password, true), 0, 32);
        $iv = chr(0x0) . chr(0x0) . chr(0x43) . chr(0x24) . chr(0x15) . chr(0x14) . chr(0x0) . chr(0x0) . chr(0x48) . chr(0x0) . chr(0x0) . chr(0x0) . chr(0x19) . chr(0x0) . chr(0x17) . chr(0x0);
        return base64_encode(\openssl_encrypt($string_to_encrypt, $method, $key, OPENSSL_RAW_DATA, $iv));
    }
}
?>
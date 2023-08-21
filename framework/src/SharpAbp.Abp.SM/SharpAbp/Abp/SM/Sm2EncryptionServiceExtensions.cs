﻿using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SharpAbp.Abp.SM
{
    public static class Sm2EncryptionServiceExtensions
    {
        /// <summary>
        /// 生成SM2密钥对
        /// </summary>
        /// <param name="sm2EncryptionService"></param>
        /// <param name="curve">曲率名称,默认使用:Sm2p256v1</param>
        /// <param name="rd">随机数</param>
        /// <returns></returns>
        public static (string, string) GenerateStringKeyPair(
            this ISm2EncryptionService sm2EncryptionService,
            string curve = Sm2EncryptionNames.CurveSm2p256v1,
            SecureRandom rd = null)
        {
            var keyPair = sm2EncryptionService.GenerateKeyPair(curve, rd);
            var aPub = ((ECPublicKeyParameters)keyPair.Public).Q.GetEncoded();
            var aPriv = ((ECPrivateKeyParameters)keyPair.Private).D.ToByteArray();

            var publicKey = Hex.ToHexString(aPub);
            var privateKey = Hex.ToHexString(aPriv);
            return (publicKey, privateKey);
        }

        /// <summary>
        /// 使用公钥进行加密
        /// </summary>
        /// <param name="sm2EncryptionService"></param>
        /// <param name="publicKey">公钥</param>
        /// <param name="plainText">明文</param>
        /// <param name="codeName">编码,默认utf-8</param>
        /// <param name="curve">曲率名称</param>
        /// <param name="mode">加密模式</param>
        /// <returns></returns>
        public static string Encrypt(
            this ISm2EncryptionService sm2EncryptionService,
            string publicKey,
            string plainText,
            string codeName = "utf-8",
            string curve = Sm2EncryptionNames.CurveSm2p256v1,
            Mode mode = Mode.C1C2C3)
        {
            var pub = Hex.DecodeStrict(publicKey.ToLower());
            var buffer = Encoding.GetEncoding(codeName).GetBytes(plainText);
            var v = sm2EncryptionService.Encrypt(pub, buffer, curve, mode);
            return Hex.ToHexString(v);
        }

        /// <summary>
        /// 使用私钥进行解密
        /// </summary>
        /// <param name="sm2EncryptionService"></param>
        /// <param name="privateKey">私钥</param>
        /// <param name="cipherText">密文</param>
        /// <param name="codeName">编码,默认utf-8</param>
        /// <param name="curve">曲率名称</param>
        /// <param name="mode">加密模式</param>
        /// <returns></returns>
        public static string Decrypt(
            this ISm2EncryptionService sm2EncryptionService,
            string privateKey,
            string cipherText,
            string codeName = "utf-8",
            string curve = Sm2EncryptionNames.CurveSm2p256v1,
            Mode mode = Mode.C1C2C3)
        {
            var aPriv = Hex.DecodeStrict(privateKey.ToLower());
            var buffer = Hex.DecodeStrict(cipherText);
            var v = sm2EncryptionService.Decrypt(aPriv, buffer, curve, mode);
            return Encoding.GetEncoding(codeName).GetString(v);
        }

        /// <summary>
        /// 使用私钥进行加签
        /// </summary>
        /// <param name="sm2EncryptionService"></param>
        /// <param name="privateKey">私钥</param>
        /// <param name="plainText">明文</param>
        /// <param name="codeName">编码,默认utf-8</param>
        /// <param name="curve">曲率名称</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static string Sign(
            this ISm2EncryptionService sm2EncryptionService,
            string privateKey,
            string plainText,
            string codeName = "utf-8",
            string curve = Sm2EncryptionNames.CurveSm2p256v1,
            byte[] id = null)
        {
            var aPriv = Hex.Decode(Encoding.Default.GetBytes(privateKey));
            var buffer = Encoding.GetEncoding(codeName).GetBytes(plainText);
            var v = sm2EncryptionService.Sign(aPriv, buffer, curve, id);
            return Hex.ToHexString(v);
        }

        /// <summary>
        /// 使用公钥进行验签
        /// </summary>
        /// <param name="sm2EncryptionService"></param>
        /// <param name="publicKey">公钥</param>
        /// <param name="plainText">明文</param>
        /// <param name="signature">签名数据</param>
        /// <param name="codeName">编码,默认utf-8</param>
        /// <param name="curve">曲率名称</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static bool VerifySign(
            this ISm2EncryptionService sm2EncryptionService,
            string publicKey,
            string plainText,
            string signature,
            string codeName = "utf-8",
            string curve = Sm2EncryptionNames.CurveSm2p256v1,
            byte[] id = null)
        {
            var aPub = Hex.DecodeStrict(publicKey);
            var s = Hex.DecodeStrict(signature);
            var buffer = Encoding.GetEncoding(codeName).GetBytes(plainText);
            return sm2EncryptionService.VerifySign(aPub, buffer, s, curve, id);
        }

    }
}

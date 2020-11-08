﻿using System;

namespace SharpAbp.Abp.Micro
{
    public class ServiceHostAndPort : IEquatable<ServiceHostAndPort>
    {
        public string Host { get; }
        public int Port { get; }

        public ServiceHostAndPort(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public bool Equals(ServiceHostAndPort other)
        {
            if (other is null)
            {
                return false;
            }
            return Host == other.Host && Port == other.Port;
        }


        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is ServiceHostAndPort other && Equals(other);
        }

        public override int GetHashCode()
        {
            return StringComparer.InvariantCulture.GetHashCode(Host) | Port.GetHashCode();
        }

        public static bool operator ==(ServiceHostAndPort s1, ServiceHostAndPort s2) => s1 != null && s2 != null && s1.Equals(s2);

        public static bool operator !=(ServiceHostAndPort s1, ServiceHostAndPort s2) => s1 != null && s2 != null && !s1.Equals(s2);
    }
}

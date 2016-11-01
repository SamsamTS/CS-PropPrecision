using System.Reflection;
using ColossalFramework;
using PropPrecision.Redirection;
using UnityEngine;

namespace PropPrecision
{
    [TargetType(typeof(PropInstance))]
    public struct PropInstanceDetour
    {
        [RedirectMethod]
        public Vector3 Position
        {
            get
            {
                if (Singleton<ToolManager>.instance.m_properties.m_mode == ItemClass.Availability.AssetEditor)
                {
                    Vector3 result;
                    result.x = (float)this.m_posX * 0.0164794922f;
                    result.y = (float)this.m_posY * 0.015625f;
                    result.z = (float)this.m_posZ * 0.0164794922f;
                    return result;
                }
                else
                {
                    Vector3 result;
                    //Begin Mod
                    //result.x = (float)this.m_posX * 0.263671875f;
                    //result.y = (float)this.m_posY * 0.015625f;
                    //result.z = (float)this.m_posZ * 0.263671875f;

                    if (Data.data.ContainsKey(m_propIndex))
                    {
                        Data.PrecisionData precisionData = (Data.PrecisionData)Data.data[m_propIndex];

                        result.x = ((float)this.m_posX + Mathf.Sign(this.m_posX) * (float)precisionData.x / (float)ushort.MaxValue) * 0.263671875f;
                        result.y = (float)this.m_posY * 0.015625f;
                        result.z = ((float)this.m_posZ + Mathf.Sign(this.m_posZ) * (float)precisionData.z / (float)ushort.MaxValue) * 0.263671875f;
                    }
                    else
                    {
                        result.x = (float)this.m_posX * 0.263671875f;
                        result.y = (float)this.m_posY * 0.015625f;
                        result.z = (float)this.m_posZ * 0.263671875f;
                    }
                    //EndMod
                    return result;
                }
            }
            set
            {
                if (Singleton<ToolManager>.instance.m_properties.m_mode == ItemClass.Availability.AssetEditor)
                {
                    this.m_posX = (short)Mathf.Clamp(Mathf.RoundToInt(value.x * 60.68148f), -32767, 32767);
                    this.m_posZ = (short)Mathf.Clamp(Mathf.RoundToInt(value.z * 60.68148f), -32767, 32767);
                    this.m_posY = (ushort)Mathf.Clamp(Mathf.RoundToInt(value.y * 64f), 0, 65535);
                }
                else
                {
                    this.m_posX = (short)Mathf.Clamp((int)(value.x * 3.79259253f), -32767, 32767);
                    this.m_posZ = (short)Mathf.Clamp((int)(value.z * 3.79259253f), -32767, 32767);
                    this.m_posY = (ushort)Mathf.Clamp(Mathf.RoundToInt(value.y * 64f), 0, 65535);
                    //Begin Mod
                        Data.PrecisionData precisionData = new Data.PrecisionData();
                        precisionData.x = (ushort)(ushort.MaxValue * Mathf.Abs(value.x * 3.79259253f - (float)this.m_posX));
                        precisionData.z = (ushort)(ushort.MaxValue * Mathf.Abs(value.z * 3.79259253f - (float)this.m_posZ));
                        Data.data[m_propIndex] = precisionData;
                    //EndMod
                }
            }
        }

        private ushort m_propIndex
        {
            get
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        fixed (PropInstance* buffer = PropManager.instance.m_props.m_buffer)
                        {
                            PropInstance* prop = (PropInstance*)pointer;
                            return (ushort)(prop - buffer);
                        }
                    }
                }
            }
        }

        private ushort m_infoIndex
        {
            get
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        return prop->m_infoIndex;
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        prop->m_infoIndex = value;
                    }
                }
            }
        }

        private short m_posX
        {
            get
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        return prop->m_posX;
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        prop->m_posX = value;
                    }
                }
            }
        }

        private ushort m_posY
        {
            get
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        return prop->m_posY;
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        prop->m_posY = value;
                    }
                }
            }
        }

        private short m_posZ
        {
            get
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        return prop->m_posZ;
                    }
                }
            }
            set
            {
                unsafe
                {
                    fixed (void* pointer = &this)
                    {
                        PropInstance* prop = (PropInstance*)pointer;
                        prop->m_posZ = value;
                    }
                }
            }
        }
    }
}
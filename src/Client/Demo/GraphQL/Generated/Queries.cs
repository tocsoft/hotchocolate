﻿using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace StrawberryShake.Client
{
    public class Queries
        : IDocument
    {
        private readonly byte[] _hashName = new byte[]
        {
            109,
            100,
            53,
            72,
            97,
            115,
            104
        };
        private readonly byte[] _hash = new byte[]
        {
            77,
            66,
            87,
            86,
            52,
            53,
            86,
            48,
            89,
            115,
            104,
            115,
            68,
            98,
            119,
            119,
            81,
            87,
            43,
            113,
            52,
            119,
            61,
            61
        };
        private readonly byte[] _content = new byte[]
        {
            113,
            117,
            101,
            114,
            121,
            32,
            103,
            101,
            116,
            72,
            101,
            114,
            111,
            32,
            123,
            32,
            104,
            101,
            114,
            111,
            32,
            123,
            32,
            46,
            46,
            46,
            32,
            72,
            101,
            114,
            111,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            102,
            114,
            97,
            103,
            109,
            101,
            110,
            116,
            32,
            72,
            101,
            114,
            111,
            32,
            111,
            110,
            32,
            67,
            104,
            97,
            114,
            97,
            99,
            116,
            101,
            114,
            32,
            123,
            32,
            46,
            46,
            46,
            32,
            72,
            97,
            115,
            78,
            97,
            109,
            101,
            32,
            46,
            46,
            46,
            32,
            72,
            97,
            115,
            70,
            114,
            105,
            101,
            110,
            100,
            115,
            32,
            104,
            101,
            105,
            103,
            104,
            116,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            102,
            114,
            97,
            103,
            109,
            101,
            110,
            116,
            32,
            70,
            114,
            105,
            101,
            110,
            100,
            32,
            111,
            110,
            32,
            67,
            104,
            97,
            114,
            97,
            99,
            116,
            101,
            114,
            67,
            111,
            110,
            110,
            101,
            99,
            116,
            105,
            111,
            110,
            32,
            123,
            32,
            110,
            111,
            100,
            101,
            115,
            32,
            123,
            32,
            46,
            46,
            46,
            32,
            72,
            97,
            115,
            78,
            97,
            109,
            101,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            102,
            114,
            97,
            103,
            109,
            101,
            110,
            116,
            32,
            72,
            97,
            115,
            78,
            97,
            109,
            101,
            32,
            111,
            110,
            32,
            67,
            104,
            97,
            114,
            97,
            99,
            116,
            101,
            114,
            32,
            123,
            32,
            110,
            97,
            109,
            101,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            102,
            114,
            97,
            103,
            109,
            101,
            110,
            116,
            32,
            72,
            97,
            115,
            70,
            114,
            105,
            101,
            110,
            100,
            115,
            32,
            111,
            110,
            32,
            67,
            104,
            97,
            114,
            97,
            99,
            116,
            101,
            114,
            32,
            123,
            32,
            102,
            114,
            105,
            101,
            110,
            100,
            115,
            32,
            123,
            32,
            46,
            46,
            46,
            32,
            70,
            114,
            105,
            101,
            110,
            100,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            125
        };

        public ReadOnlySpan<byte> HashName => _hashName;

        public ReadOnlySpan<byte> Hash => _hash;

        public ReadOnlySpan<byte> Content => _content;

        public static Queries Default { get; } = new Queries();

        public override string ToString() => 
            @"query getHero {
              hero {
                ... Hero
              }
            }
            
            fragment Hero on Character {
              ... HasName
              ... HasFriends
              height
            }
            
            fragment Friend on CharacterConnection {
              nodes {
                ... HasName
              }
            }
            
            fragment HasName on Character {
              name
            }
            
            fragment HasFriends on Character {
              friends {
                ... Friend
              }
            }";
    }
}
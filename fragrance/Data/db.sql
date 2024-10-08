PGDMP  %    -                |            millisoft-project    16.3    16.3 >    W           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            X           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            Y           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            Z           1262    24590    millisoft-project    DATABASE     u   CREATE DATABASE "millisoft-project" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'C';
 #   DROP DATABASE "millisoft-project";
                postgres    false            �            1259    34812    favorite_items    TABLE     �   CREATE TABLE public.favorite_items (
    favorite_item_id integer NOT NULL,
    favorite_item_fragrance_id integer,
    favorite_item_user_id integer,
    favorite_item_date date DEFAULT CURRENT_TIMESTAMP
);
 "   DROP TABLE public.favorite_items;
       public         heap    postgres    false            �            1259    34811 #   favorite_items_favorite_item_id_seq    SEQUENCE     �   CREATE SEQUENCE public.favorite_items_favorite_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 :   DROP SEQUENCE public.favorite_items_favorite_item_id_seq;
       public          postgres    false    228            [           0    0 #   favorite_items_favorite_item_id_seq    SEQUENCE OWNED BY     k   ALTER SEQUENCE public.favorite_items_favorite_item_id_seq OWNED BY public.favorite_items.favorite_item_id;
          public          postgres    false    227            �            1259    34658    fragrance_images    TABLE     �   CREATE TABLE public.fragrance_images (
    fragrance_image_id integer NOT NULL,
    fragrance_image_url text,
    fragrance_id integer
);
 $   DROP TABLE public.fragrance_images;
       public         heap    postgres    false            �            1259    34657 '   fragrance_images_fragrance_image_id_seq    SEQUENCE     �   CREATE SEQUENCE public.fragrance_images_fragrance_image_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 >   DROP SEQUENCE public.fragrance_images_fragrance_image_id_seq;
       public          postgres    false    220            \           0    0 '   fragrance_images_fragrance_image_id_seq    SEQUENCE OWNED BY     s   ALTER SEQUENCE public.fragrance_images_fragrance_image_id_seq OWNED BY public.fragrance_images.fragrance_image_id;
          public          postgres    false    219            �            1259    34674    fragrance_reviews    TABLE     h  CREATE TABLE public.fragrance_reviews (
    fragrance_review_id integer NOT NULL,
    fragrance_review_text text NOT NULL,
    fragrance_review_rating double precision,
    fragrance_review_user_id integer,
    fragrance_review_title character varying(50),
    fragrance_review_date date DEFAULT CURRENT_TIMESTAMP,
    fragrance_review_fragrance_id integer
);
 %   DROP TABLE public.fragrance_reviews;
       public         heap    postgres    false            �            1259    34673 )   fragrance_reviews_fragrance_review_id_seq    SEQUENCE     �   CREATE SEQUENCE public.fragrance_reviews_fragrance_review_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 @   DROP SEQUENCE public.fragrance_reviews_fragrance_review_id_seq;
       public          postgres    false    222            ]           0    0 )   fragrance_reviews_fragrance_review_id_seq    SEQUENCE OWNED BY     w   ALTER SEQUENCE public.fragrance_reviews_fragrance_review_id_seq OWNED BY public.fragrance_reviews.fragrance_review_id;
          public          postgres    false    221            �            1259    34618 
   fragrances    TABLE     �  CREATE TABLE public.fragrances (
    fragrance_id integer NOT NULL,
    fragrance_brand character varying(20) NOT NULL,
    fragrance_line character varying(20) NOT NULL,
    fragrance_price integer NOT NULL,
    fragrance_gender smallint,
    fragrance_image text,
    fragrance_old_price integer,
    created_at date DEFAULT CURRENT_TIMESTAMP,
    updated_at date DEFAULT CURRENT_TIMESTAMP,
    fragrance_desc text,
    fragrance_stock integer DEFAULT 0,
    fragrance_long_desc text
);
    DROP TABLE public.fragrances;
       public         heap    postgres    false            �            1259    34617    fragrances_fragrance_id_seq    SEQUENCE     �   CREATE SEQUENCE public.fragrances_fragrance_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.fragrances_fragrance_id_seq;
       public          postgres    false    218            ^           0    0    fragrances_fragrance_id_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.fragrances_fragrance_id_seq OWNED BY public.fragrances.fragrance_id;
          public          postgres    false    217            �            1259    34787    order_items    TABLE     �   CREATE TABLE public.order_items (
    order_item_id integer NOT NULL,
    order_item_order_id integer,
    order_item_fragrance_id integer,
    order_item_quantity integer,
    order_item_unit_price integer
);
    DROP TABLE public.order_items;
       public         heap    postgres    false            �            1259    34786    order_tems_order_item_id_seq    SEQUENCE     �   CREATE SEQUENCE public.order_tems_order_item_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public.order_tems_order_item_id_seq;
       public          postgres    false    226            _           0    0    order_tems_order_item_id_seq    SEQUENCE OWNED BY     ^   ALTER SEQUENCE public.order_tems_order_item_id_seq OWNED BY public.order_items.order_item_id;
          public          postgres    false    225            �            1259    34774    orders    TABLE     �   CREATE TABLE public.orders (
    order_id integer NOT NULL,
    order_user_id integer,
    order_date date DEFAULT CURRENT_TIMESTAMP,
    order_total_price integer,
    order_status character varying(20) DEFAULT 'Pending'::character varying
);
    DROP TABLE public.orders;
       public         heap    postgres    false            �            1259    34773    orders_order_id_seq    SEQUENCE     �   CREATE SEQUENCE public.orders_order_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.orders_order_id_seq;
       public          postgres    false    224            `           0    0    orders_order_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.orders_order_id_seq OWNED BY public.orders.order_id;
          public          postgres    false    223            �            1259    34610    users    TABLE     j  CREATE TABLE public.users (
    user_id integer NOT NULL,
    user_name character varying(20) NOT NULL,
    user_surname character varying(20) NOT NULL,
    user_gender smallint,
    user_email character varying(50) NOT NULL,
    user_password character varying(60) NOT NULL,
    user_role character varying(10) DEFAULT 'customer'::character varying NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false            �            1259    34609    users_user_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.users_user_id_seq;
       public          postgres    false    216            a           0    0    users_user_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;
          public          postgres    false    215            �           2604    34815    favorite_items favorite_item_id    DEFAULT     �   ALTER TABLE ONLY public.favorite_items ALTER COLUMN favorite_item_id SET DEFAULT nextval('public.favorite_items_favorite_item_id_seq'::regclass);
 N   ALTER TABLE public.favorite_items ALTER COLUMN favorite_item_id DROP DEFAULT;
       public          postgres    false    227    228    228            �           2604    34661 #   fragrance_images fragrance_image_id    DEFAULT     �   ALTER TABLE ONLY public.fragrance_images ALTER COLUMN fragrance_image_id SET DEFAULT nextval('public.fragrance_images_fragrance_image_id_seq'::regclass);
 R   ALTER TABLE public.fragrance_images ALTER COLUMN fragrance_image_id DROP DEFAULT;
       public          postgres    false    220    219    220            �           2604    34677 %   fragrance_reviews fragrance_review_id    DEFAULT     �   ALTER TABLE ONLY public.fragrance_reviews ALTER COLUMN fragrance_review_id SET DEFAULT nextval('public.fragrance_reviews_fragrance_review_id_seq'::regclass);
 T   ALTER TABLE public.fragrance_reviews ALTER COLUMN fragrance_review_id DROP DEFAULT;
       public          postgres    false    221    222    222            �           2604    34621    fragrances fragrance_id    DEFAULT     �   ALTER TABLE ONLY public.fragrances ALTER COLUMN fragrance_id SET DEFAULT nextval('public.fragrances_fragrance_id_seq'::regclass);
 F   ALTER TABLE public.fragrances ALTER COLUMN fragrance_id DROP DEFAULT;
       public          postgres    false    217    218    218            �           2604    34790    order_items order_item_id    DEFAULT     �   ALTER TABLE ONLY public.order_items ALTER COLUMN order_item_id SET DEFAULT nextval('public.order_tems_order_item_id_seq'::regclass);
 H   ALTER TABLE public.order_items ALTER COLUMN order_item_id DROP DEFAULT;
       public          postgres    false    225    226    226            �           2604    34777    orders order_id    DEFAULT     r   ALTER TABLE ONLY public.orders ALTER COLUMN order_id SET DEFAULT nextval('public.orders_order_id_seq'::regclass);
 >   ALTER TABLE public.orders ALTER COLUMN order_id DROP DEFAULT;
       public          postgres    false    224    223    224            �           2604    34613    users user_id    DEFAULT     n   ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);
 <   ALTER TABLE public.users ALTER COLUMN user_id DROP DEFAULT;
       public          postgres    false    216    215    216            T          0    34812    favorite_items 
   TABLE DATA           �   COPY public.favorite_items (favorite_item_id, favorite_item_fragrance_id, favorite_item_user_id, favorite_item_date) FROM stdin;
    public          postgres    false    228   �R       L          0    34658    fragrance_images 
   TABLE DATA           a   COPY public.fragrance_images (fragrance_image_id, fragrance_image_url, fragrance_id) FROM stdin;
    public          postgres    false    220   S       N          0    34674    fragrance_reviews 
   TABLE DATA           �   COPY public.fragrance_reviews (fragrance_review_id, fragrance_review_text, fragrance_review_rating, fragrance_review_user_id, fragrance_review_title, fragrance_review_date, fragrance_review_fragrance_id) FROM stdin;
    public          postgres    false    222   9S       J          0    34618 
   fragrances 
   TABLE DATA           �   COPY public.fragrances (fragrance_id, fragrance_brand, fragrance_line, fragrance_price, fragrance_gender, fragrance_image, fragrance_old_price, created_at, updated_at, fragrance_desc, fragrance_stock, fragrance_long_desc) FROM stdin;
    public          postgres    false    218   �S       R          0    34787    order_items 
   TABLE DATA           �   COPY public.order_items (order_item_id, order_item_order_id, order_item_fragrance_id, order_item_quantity, order_item_unit_price) FROM stdin;
    public          postgres    false    226   �_       P          0    34774    orders 
   TABLE DATA           f   COPY public.orders (order_id, order_user_id, order_date, order_total_price, order_status) FROM stdin;
    public          postgres    false    224   �_       H          0    34610    users 
   TABLE DATA           t   COPY public.users (user_id, user_name, user_surname, user_gender, user_email, user_password, user_role) FROM stdin;
    public          postgres    false    216   �_       b           0    0 #   favorite_items_favorite_item_id_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public.favorite_items_favorite_item_id_seq', 25, true);
          public          postgres    false    227            c           0    0 '   fragrance_images_fragrance_image_id_seq    SEQUENCE SET     V   SELECT pg_catalog.setval('public.fragrance_images_fragrance_image_id_seq', 77, true);
          public          postgres    false    219            d           0    0 )   fragrance_reviews_fragrance_review_id_seq    SEQUENCE SET     W   SELECT pg_catalog.setval('public.fragrance_reviews_fragrance_review_id_seq', 5, true);
          public          postgres    false    221            e           0    0    fragrances_fragrance_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public.fragrances_fragrance_id_seq', 33, true);
          public          postgres    false    217            f           0    0    order_tems_order_item_id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.order_tems_order_item_id_seq', 45, true);
          public          postgres    false    225            g           0    0    orders_order_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.orders_order_id_seq', 22, true);
          public          postgres    false    223            h           0    0    users_user_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.users_user_id_seq', 20, true);
          public          postgres    false    215            �           2606    34818 "   favorite_items favorite_items_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public.favorite_items
    ADD CONSTRAINT favorite_items_pkey PRIMARY KEY (favorite_item_id);
 L   ALTER TABLE ONLY public.favorite_items DROP CONSTRAINT favorite_items_pkey;
       public            postgres    false    228            �           2606    34665 &   fragrance_images fragrance_images_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public.fragrance_images
    ADD CONSTRAINT fragrance_images_pkey PRIMARY KEY (fragrance_image_id);
 P   ALTER TABLE ONLY public.fragrance_images DROP CONSTRAINT fragrance_images_pkey;
       public            postgres    false    220            �           2606    34681 (   fragrance_reviews fragrance_reviews_pkey 
   CONSTRAINT     w   ALTER TABLE ONLY public.fragrance_reviews
    ADD CONSTRAINT fragrance_reviews_pkey PRIMARY KEY (fragrance_review_id);
 R   ALTER TABLE ONLY public.fragrance_reviews DROP CONSTRAINT fragrance_reviews_pkey;
       public            postgres    false    222            �           2606    34623    fragrances fragrances_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.fragrances
    ADD CONSTRAINT fragrances_pkey PRIMARY KEY (fragrance_id);
 D   ALTER TABLE ONLY public.fragrances DROP CONSTRAINT fragrances_pkey;
       public            postgres    false    218            �           2606    34792    order_items order_tems_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_tems_pkey PRIMARY KEY (order_item_id);
 E   ALTER TABLE ONLY public.order_items DROP CONSTRAINT order_tems_pkey;
       public            postgres    false    226            �           2606    34780    orders orders_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (order_id);
 <   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_pkey;
       public            postgres    false    224            �           2606    34695    users unique_user_email 
   CONSTRAINT     X   ALTER TABLE ONLY public.users
    ADD CONSTRAINT unique_user_email UNIQUE (user_email);
 A   ALTER TABLE ONLY public.users DROP CONSTRAINT unique_user_email;
       public            postgres    false    216            �           2606    34616    users users_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    216            �           2606    34819 =   favorite_items favorite_items_favorite_item_fragrance_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.favorite_items
    ADD CONSTRAINT favorite_items_favorite_item_fragrance_id_fkey FOREIGN KEY (favorite_item_fragrance_id) REFERENCES public.fragrances(fragrance_id);
 g   ALTER TABLE ONLY public.favorite_items DROP CONSTRAINT favorite_items_favorite_item_fragrance_id_fkey;
       public          postgres    false    3493    228    218            �           2606    34824 8   favorite_items favorite_items_favorite_item_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.favorite_items
    ADD CONSTRAINT favorite_items_favorite_item_user_id_fkey FOREIGN KEY (favorite_item_user_id) REFERENCES public.users(user_id);
 b   ALTER TABLE ONLY public.favorite_items DROP CONSTRAINT favorite_items_favorite_item_user_id_fkey;
       public          postgres    false    228    216    3491            �           2606    34832 2   fragrance_reviews fk_fragrance_review_fragrance_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.fragrance_reviews
    ADD CONSTRAINT fk_fragrance_review_fragrance_id FOREIGN KEY (fragrance_review_fragrance_id) REFERENCES public.fragrances(fragrance_id);
 \   ALTER TABLE ONLY public.fragrance_reviews DROP CONSTRAINT fk_fragrance_review_fragrance_id;
       public          postgres    false    222    3493    218            �           2606    34666 3   fragrance_images fragrance_images_fragrance_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.fragrance_images
    ADD CONSTRAINT fragrance_images_fragrance_id_fkey FOREIGN KEY (fragrance_id) REFERENCES public.fragrances(fragrance_id);
 ]   ALTER TABLE ONLY public.fragrance_images DROP CONSTRAINT fragrance_images_fragrance_id_fkey;
       public          postgres    false    220    3493    218            �           2606    34682 A   fragrance_reviews fragrance_reviews_fragrance_review_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.fragrance_reviews
    ADD CONSTRAINT fragrance_reviews_fragrance_review_user_id_fkey FOREIGN KEY (fragrance_review_user_id) REFERENCES public.users(user_id);
 k   ALTER TABLE ONLY public.fragrance_reviews DROP CONSTRAINT fragrance_reviews_fragrance_review_user_id_fkey;
       public          postgres    false    222    3491    216            �           2606    34798 3   order_items order_tems_order_item_fragrance_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_tems_order_item_fragrance_id_fkey FOREIGN KEY (order_item_fragrance_id) REFERENCES public.fragrances(fragrance_id);
 ]   ALTER TABLE ONLY public.order_items DROP CONSTRAINT order_tems_order_item_fragrance_id_fkey;
       public          postgres    false    3493    218    226            �           2606    34793 /   order_items order_tems_order_item_order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_tems_order_item_order_id_fkey FOREIGN KEY (order_item_order_id) REFERENCES public.orders(order_id);
 Y   ALTER TABLE ONLY public.order_items DROP CONSTRAINT order_tems_order_item_order_id_fkey;
       public          postgres    false    3499    224    226            �           2606    34781     orders orders_order_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_order_user_id_fkey FOREIGN KEY (order_user_id) REFERENCES public.users(user_id);
 J   ALTER TABLE ONLY public.orders DROP CONSTRAINT orders_order_user_id_fkey;
       public          postgres    false    3491    224    216            T   =   x�]ȱ�0����mA�߅��@H(坅7���Ui�X{�v	&<���d.���}�| G�Y      L      x������ � �      N   Z   x�3�t/JM,Q(H,J+�MU�4�42�t�,*.Q(J-�L-W
��X��s�s�r�d��(�df�rrZr楦'�d���)����� ��      J   �  x��Y�r�6}��/��h�Ҍ�T��|������l��*H�$< � ���/����� ɡƒ-۩u�F#�ht��>�,΢�)��]cK���^,��<*���w�Y�V�D�R[���j���b�]��-t=��N����jq:[�֫����d5����}�Go_G���t2?�,��_���i�x�2��+Z��^%���;fD�d�/�߱Z�a:cw4������&-�_���t�x#Vb��k��F�c�)Y�m��~����m"*G:��d؁e��MJ^�]��QntS�"=f;iE��
Q��%���J��i�̫?=:�~�
ˮ8�e�V.���5��Zv���,Et1FQq����?g�买��+%L�����|9;9;9�PVT��ʫ����L��U>��`�N����)��}ه����-�5a@����~t	7)/uy��rC�ܞ��FV��^�!�:$B�E����-�z�LID�]��Z�Rbzt]�=�&�+�l�O&)d-NV_�y�gg��) [�?0�*��F���u�H�ؑ�w�=#���UƓ:[�:��x �C(n� z��$��E��A��&�4r����W�����1�:�Tsӄ}�Er�GX�A�RX��n*^k�s��
�Yc�ʦG��E�$�R�6����4m�인��Os��	�,������w�^�/�����z���w��糳W�E�������je��˧?c?��)�n�B#b�ԁ˃�� ool��T �3�SB.~Jd@��u*�D��4Hj���V
透�Y�KԺ�7P��@�j�u�=�^�j�j w 9@Rs��Q�S�k�����$T��ʔ8���7���X��ލ�r���jC�a/x�G�2/B���Y�E��>��-.���Er+�ڶ�&)�����!g?�T��X.�՚itzhۛ���6���L|�ΣG����� ,���To+Qj01�k�U�[�}V�JN������[<FlQ�-^�5A}�
Z!9U�_�`�,����b>Fz��Mkn��$P�����r�g�$�ܿ-�[)&���?�ZfM(}��}-���y;�A��o�_`��
vhE��kw�G���!U���{�s[ʪCLCnNٌ����Ca��@��C��!k�V����X���"���5{�=#��7�H��q;Աq0ρw��D���I��ӝL]��b=�����{�=V�Y�����q�>�g�b��
�0��{��U~��� �J�n�n���K�p��C?Q9Q��i��x�E�֡����}ҵ\_'}��VLtٛȪ�!�	F|p��D�TP�@,`Q�Pv״QH��A曾� ��J�G�y��@���d�X��r�M�M�xVϐ�����&�i��O����<[��_����\,O/ҳ��ŧC3���|r_��)҃_6G��Z���F��Yb4bpT׮iN�i|�|E(�i��o��؊���`&���?c�\��B[�8�~�TP��7Ze�sE�ź���j���uW?�Y?�V��s��'��g���Ӌ:��S�r�6������U�f����D74�79�B������r;�'�^]�6���Z毓���]�ӾW�G�G�q��F�"��NB�=�J�-=V��!N�.?�u��11����w��~���q���c�yf��&��8-�pNz~`c���>8^���t�4Ə%C�|��T�Q�w	�K>�þv�;��t���0�<rP����V�+�Eڂ�eײ��yk;��U��B��D�S�Mtś-0�οx"^/�h�����V�M�ܽy.Q��R��ў~��;��b���'R�5d��r\V[�#]?U���@�fE�y���陠;�ЍK�6}�M4��Qo1-��Ӏ�]4�!\�5��]̾�O�C�E��	�Xץ1���`1=���h�B]~��4�����^����d=�������TdT5��P􁕺Ach��z����u�
�����luG��x����44^��;����b8� ?<&8C��M�^�D�6zʥm�_^�߼Z$�?$O{����\���ۺ�i���W��r�J4�f:��琦�z��(�
},g��HU��k-#�Lc޴��_���Cq?����qHxO�� }�"�g��@ɟ3m>	�P�lk�$��k(����������?��U�!N�W��.�Ɍ���N6$t":�(<�C>�����R�~"���9݇'��fx��ہ;��������70��U����`���Ԁo��D:C�ρJ�?�W~���O�� ��<,t�.��3�^�M����B�H�e��@�赞��Ϭ/=�
v�^�z������<<�w�	<�M3c^u�3����ӷ�U/��@A��-��x����h���%:�W�f�gh�C_���MaI��N�`�_9����ɭ��"���?�D��yU��%���5��9�,��`�͈d�(�MC�?���<9u����N�������#�O���u���9f�:�o��뚽�Hk2ړ�� \F�k��6�|B�� ��tʳ��9�xpA�+�M0�`]?↧�����-�wf���9<ʮIת�(�@n���К{"0�5��x������`�AU�o:� .�x4Iz���G��A���U��[�zND@_����溭��2����5e��k
+���袉o�m�ctكM9}�0��� ��O8]�;��O�K�psC50�k>���)L�y�@�?�G7Ą�:�Y�Jp�����dY��� /��n!��߈�b�R	̂�S��}}���ߌ��F��7�ǒ�yW���;�p��p�%��&��t��3���'=�p9�b��
�^96s^3T~�SY�?E7�t7:��V��m�'[t���Ս�D璊M,
C�R�x�a�R�d�^;;��<;�RO�Q�����DH[�ZH
Ƈ���j��.����l�"��:b����$ܥx��
{oY|ֳB���o��6�"�C4���bӣ�ӣ���l�P�      R      x������ � �      P      x������ � �      H   7  x�m�Ks�0 ���W����7�Q�t�Ę"	�#¯o�v<�����~��L0:f1{(��Ê��Bxd�������r��4��j~��إU��I?w�N�����'���x#�O����i�;�u8��~�{F�����]b�j�,=Hr��՘"ŢF>JҨ.�A�/�g�9��u|, �ˊg�"!���	�	~�ʥ����:Ԃ�8���=t�VL�K)�i��KG��qRM:�_�F]��:gN�����_zA����F�����H��?��YĢ���MS�z�s����hS
zHf[���+�$}J��=     
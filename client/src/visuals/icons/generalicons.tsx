type IconProps = {
    iconName?: string;
}

function Icons(props: IconProps) {
    switch (props.iconName) {
        case "Logo":
            return (
                <svg version="1.0" xmlns="http://www.w3.org/2000/svg"
                    width="70.000000pt" height="62.000000pt" viewBox="0 0 175.000000 162.000000"
                    preserveAspectRatio="xMidYMid meet">
                    <metadata>
                        Created by potrace 1.16, written by Peter Selinger 2001-2019
                    </metadata>
                    <g transform="translate(-30.000000,162.000000) scale(0.100000,-0.100000)"
                        fill="#000000" stroke="none">
                        <path d="M920 1299 c0 -19 5 -60 10 -90 9 -47 6 -72 -15 -167 -13 -62 -38
-146 -55 -189 -43 -108 -32 -113 17 -7 24 54 43 112 48 148 7 61 22 106 36
106 16 0 99 -143 99 -170 0 -7 -38 -69 -84 -138 -46 -70 -97 -152 -114 -182
-17 -31 -46 -70 -63 -88 -39 -39 -96 -82 -83 -61 5 7 9 16 10 19 11 37 20 48
46 58 16 6 27 16 23 22 -4 7 -12 7 -25 0 -26 -14 -26 -14 -24 17 0 15 6 29 12
31 15 5 16 32 2 32 -7 0 -6 8 2 23 7 12 12 54 13 92 0 61 4 76 31 115 28 42
81 196 71 207 -3 2 -31 -43 -62 -101 -31 -58 -75 -124 -99 -148 -27 -27 -57
-73 -78 -122 -38 -83 -80 -125 -105 -104 -9 7 -10 14 -3 23 14 17 2 45 -16 38
-24 -9 -16 6 39 81 29 39 63 87 75 107 l22 37 -24 6 c-17 4 -28 17 -36 43 -7
21 -16 42 -20 47 -10 10 -53 -57 -91 -140 -17 -37 -36 -63 -47 -66 -13 -4 -39
-45 -76 -120 -31 -63 -56 -119 -56 -125 0 -14 92 -143 121 -170 17 -15 42 -23
84 -26 226 -20 348 -28 359 -24 23 9 27 60 5 75 -10 7 -19 18 -19 24 0 6 6 5
16 -3 14 -11 15 -8 12 29 -2 23 -7 42 -13 42 -5 -1 -9 -8 -7 -17 2 -8 -1 -12
-7 -9 -6 4 -23 0 -38 -7 -16 -8 -37 -11 -47 -8 -16 6 -11 14 37 53 83 68 195
167 250 221 27 27 51 46 54 43 3 -3 14 -36 24 -73 15 -57 16 -70 5 -81 -11
-11 -18 -11 -35 -3 -11 7 -21 16 -21 21 0 6 -4 10 -9 10 -6 0 -20 -33 -33 -72
-19 -63 -48 -121 -69 -138 -10 -9 -39 -102 -34 -110 10 -16 142 -21 244 -11
87 9 110 15 138 37 38 29 112 119 139 170 l18 33 -71 103 c-82 120 -149 250
-187 363 -36 108 -45 119 -96 112 -38 -5 -42 -3 -86 46 -25 28 -56 72 -69 97
-40 75 -45 80 -45 39z m-454 -556 c-3 -8 -6 -21 -6 -29 0 -8 -6 -11 -16 -7 -9
3 -20 0 -25 -8 -6 -10 -9 -5 -9 17 0 37 4 42 36 42 18 0 24 -5 20 -15z"/>
                    </g>
                </svg>
            )
        case "Home":
            return (
                <svg width="23px" height="23px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M2 12.2039C2 9.91549 2 8.77128 2.5192 7.82274C3.0384 6.87421 3.98695 6.28551 5.88403 5.10813L7.88403 3.86687C9.88939 2.62229 10.8921 2 12 2C13.1079 2 14.1106 2.62229 16.116 3.86687L18.116 5.10812C20.0131 6.28551 20.9616 6.87421 21.4808 7.82274C22 8.77128 22 9.91549 22 12.2039V13.725C22 17.6258 22 19.5763 20.8284 20.7881C19.6569 22 17.7712 22 14 22H10C6.22876 22 4.34315 22 3.17157 20.7881C2 19.5763 2 17.6258 2 13.725V12.2039Z" stroke="#1C274C" strokeWidth="1.5" />
                    <path d="M15 18H9" stroke="#1C274C" strokeWidth="1.5" strokeLinecap="round" />
                </svg>
            );
        case "Measure":
            return (
                <svg width="23px" height="23px" viewBox="-24 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <g transform="rotate(90)">
                        <path d="M20.354 2.646l2.851 2.852-2.82 2.854-.712-.704L21.303 6H16V5h5.293l-1.647-1.646zM.794 5.502l2.852 2.852.707-.707L2.707 6H8V5H2.697l1.63-1.648-.711-.704zM1 12h22v8H1zm1 7h2v-3h1v3h1v-2h1v2h1v-5h1v5h1v-2h1v2h1v-3h1v3h1v-2h1v2h1v-5h1v5h1v-2h1v2h1v-3h1v3h1v-6H2z" /><path fill="none" d="M0 0h24v24H0z" />
                    </g>
                </svg>
            );
        case "Logbook":
            return (
                <svg width="23px" height="23px" viewBox="0 0 512 512" version="1.1" xmlns="http://www.w3.org/2000/svg">
                    <title>log</title>
                    <g id="Page-1" stroke="0" strokeWidth="0" fill="none" fillRule="evenodd">
                        <g id="log-white" fill="#000000" transform="translate(85.572501, 42.666667)">
                            <path d="M236.349632,7.10542736e-15 L1.68296533,7.10542736e-15 L1.68296533,234.666667 L44.349632,234.666667 L44.349632,192 L44.349632,169.6 L44.349632,42.6666667 L218.642965,42.6666667 L300.349632,124.373333 L300.349632,169.6 L300.349632,192 L300.349632,234.666667 L343.016299,234.666667 L343.016299,106.666667 L236.349632,7.10542736e-15 L236.349632,7.10542736e-15 Z M4.26325641e-14,405.333333 L4.26325641e-14,277.360521 L28.8096875,277.360521 L28.8096875,382.755208 L83.81,382.755208 L83.81,405.333333 L4.26325641e-14,405.333333 Z M153.17,275.102708 C173.279583,275.102708 188.692917,281.484792 199.41,294.248958 C209.705625,306.47125 214.853437,322.185625 214.853437,341.392083 C214.853437,362.404792 208.772396,379.112604 196.610312,391.515521 C186.134062,402.232604 171.653958,407.591146 153.17,407.591146 C133.060417,407.591146 117.647083,401.209062 106.93,388.444896 C96.634375,376.222604 91.4865625,360.267396 91.4865625,340.579271 C91.4865625,319.988021 97.5676042,303.490937 109.729687,291.088021 C120.266146,280.431146 134.74625,275.102708 153.17,275.102708 Z M153.079687,297.680833 C142.663646,297.680833 134.625833,302.015833 128.96625,310.685833 C123.848542,318.512917 121.289687,328.567708 121.289687,340.850208 C121.289687,355.059375 124.330208,366.0775 130.41125,373.904583 C136.131042,381.310208 143.717292,385.013021 153.17,385.013021 C163.525833,385.013021 171.59375,380.647917 177.37375,371.917708 C182.491458,364.211042 185.050312,354.035833 185.050312,341.392083 C185.050312,327.483958 182.009792,316.616354 175.92875,308.789271 C170.208958,301.383646 162.592604,297.680833 153.079687,297.680833 Z M343.91,333.715521 L343.91,399.011458 C336.564583,401.48 331.386667,403.105625 328.37625,403.888333 C319.043958,406.356875 309.019271,407.591146 298.302187,407.591146 C277.229271,407.591146 261.18375,402.292812 250.165625,391.696146 C237.943333,380.015729 231.832187,363.729375 231.832187,342.837083 C231.832187,318.813958 239.418437,300.69125 254.590937,288.468958 C265.609062,279.558125 280.480521,275.102708 299.205312,275.102708 C315.220729,275.102708 330.122292,278.022812 343.91,283.863021 L334.065937,306.350833 C327.563437,303.099583 321.87375,300.826719 316.996875,299.53224 C312.12,298.23776 306.761458,297.590521 300.92125,297.590521 C286.952917,297.590521 276.657292,302.13625 270.034375,311.227708 C264.435,318.934375 261.635312,329.079479 261.635312,341.663021 C261.635312,356.775312 265.849896,368.154687 274.279062,375.801146 C281.022396,381.942396 289.391354,385.013021 299.385937,385.013021 C305.226146,385.013021 310.765312,384.019583 316.003437,382.032708 L316.003437,356.293646 L293.967187,356.293646 L293.967187,333.715521 L343.91,333.715521 Z" id="XLS">

                            </path>
                        </g>
                    </g>
                </svg>
            );
        case "Neighbourhood":
            return (
                <svg fill="#000000" height="23px" width="23px" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 512.001 512.001">
                    <g>
                        <g>
                            <path d="M508.001,176.305l-84.902-64.351c-3.22-2.439-7.672-2.439-10.892,0L326.9,176.612c-3.086,2.339-4.334,6.387-3.1,10.058
			c1.233,3.671,4.674,6.143,8.546,6.143h20.41v46.406l-38.921-23.014V96.255h27.46c0.008,0,0.016,0,0.024,0
			c4.979,0,9.016-4.035,9.016-9.016c0-3.123-1.588-5.875-3.999-7.491l-84.902-64.351c-3.22-2.439-7.672-2.439-10.892,0
			l-85.307,64.658c-3.086,2.339-4.333,6.387-3.1,10.058s4.674,6.143,8.546,6.143h27.459v119.952l-38.92,23.014v-46.406h20.41
			c3.872,0,7.312-2.473,8.546-6.143c1.233-3.67-0.013-7.719-3.1-10.058l-85.306-64.66c-3.22-2.439-7.672-2.439-10.892,0
			L3.57,176.612c-3.086,2.339-4.333,6.387-3.1,10.058c1.233,3.671,4.674,6.143,8.546,6.143h20.431v296.606
			c0,4.98,4.037,9.016,9.016,9.016h111.733h211.588h111.733c4.979,0,9.016-4.035,9.016-9.016V192.814h20.431
			c0.008,0,0.016,0,0.024,0c4.979,0,9.016-4.035,9.016-9.016C512,180.675,510.412,177.923,508.001,176.305z M197.503,78.224
			l58.486-44.329l58.486,44.329H197.503z M295.805,96.256v68.176v41.112l-2.855-1.689l-32.372-19.143
			c-2.83-1.674-6.347-1.674-9.178,0l-35.227,20.831V96.256H295.805z M141.19,249.883l-28.043,16.583
			c-3.498,2.069-5.18,6.221-4.107,10.14c1.072,3.92,4.633,6.637,8.696,6.637h23.444v197.163H47.478V192.814h93.712V249.883z
			 M38.462,174.782h-2.627l58.487-44.329l58.487,44.329h-2.605H38.462z M352.767,480.404H159.21V283.24h193.557V480.404z
			 M150.695,265.209l1.079-0.638l59.962-35.457c0.002-0.001,0.006-0.002,0.008-0.004l17.336-10.253l26.906-15.91l105.293,62.261
			H150.695z M370.797,480.404V283.24h23.443c4.063,0,7.624-2.718,8.696-6.637c1.072-3.919-0.609-8.071-4.107-10.14l-28.043-16.583
			v-57.069h93.712v287.591H370.797z M473.514,174.782H361.771h-2.605l58.487-44.329l58.486,44.329H473.514z"/>
                        </g>
                    </g>
                    <g>
                        <g>
                            <path d="M126.018,296.935H62.628c-4.979,0-9.016,4.035-9.016,9.016v108.469c0,4.98,4.037,9.016,9.016,9.016h63.391
			c4.979,0,9.016-4.035,9.016-9.016v-54.224c0-0.004,0-0.007,0-0.012s0-0.007,0-0.012V305.95
			C135.034,300.97,130.997,296.935,126.018,296.935z M85.307,405.403H71.643v-36.204h13.664V405.403z M85.307,351.168H71.643
			v-36.202h13.664V351.168z M117.003,405.403h-13.664v-36.204h13.664V405.403z M117.003,351.168h-13.664v-36.202h13.664V351.168z"/>
                        </g>
                    </g>
                    <g>
                        <g>
                            <path d="M287.684,296.935h-63.391c-4.979,0-9.016,4.035-9.016,9.016v108.469c0,4.98,4.037,9.016,9.016,9.016h63.391
			c4.979,0,9.016-4.035,9.016-9.016V305.95C296.7,300.97,292.663,296.935,287.684,296.935z M233.308,314.966h13.664v36.202h-13.664
			V314.966z M246.973,405.403h-13.664v-36.204h13.664V405.403z M278.667,405.403h-13.664v-36.204h13.664V405.403z M278.668,351.168
			h-13.664v-36.202h13.664V351.168z"/>
                        </g>
                    </g>
                    <g>
                        <g>
                            <path d="M449.349,296.935h-63.391c-4.979,0-9.016,4.035-9.016,9.016v108.469c0,4.98,4.037,9.016,9.016,9.016h63.391
			c4.979,0,9.016-4.035,9.016-9.016v-54.224c0-0.004,0-0.007,0-0.012s0-0.007,0-0.012V305.95
			C458.364,300.97,454.328,296.935,449.349,296.935z M408.638,405.403h-13.664v-36.204h13.664V405.403z M408.638,351.168h-13.664
			v-36.202h13.664V351.168z M440.333,405.403h-13.664v-36.204h13.664V405.403z M440.333,351.168h-13.664v-36.202h13.664V351.168z"/>
                        </g>
                    </g>
                </svg>
            );
        case "Account":
            return (
                <svg width="23px" height="23px" viewBox="0 0 20 20" version="1.1" xmlns="http://www.w3.org/2000/svg">
                    <g id="layer1">
                        <path d="M 10 0 L 9.5644531 0.01953125 L 9.1328125 0.076171875 L 8.7050781 0.16992188 L 8.2890625 0.30273438 L 7.8867188 0.46875 L 7.5 0.66992188 L 7.1328125 0.90625 L 6.7871094 1.171875 L 6.4628906 1.4648438 L 6.1699219 1.7871094 L 5.9042969 2.1328125 L 5.6699219 2.5 L 5.46875 2.8886719 L 5.3027344 3.2910156 L 5.1699219 3.7070312 L 5.0761719 4.1328125 L 5.0195312 4.5664062 L 5 5.0019531 L 5.0175781 5.6816406 L 5.0742188 6.359375 L 5.1660156 7.0351562 L 5.2949219 7.703125 L 5.4589844 8.3613281 L 5.6601562 9.0136719 L 5.8984375 9.6523438 L 6.1660156 10.275391 L 6.4707031 10.884766 L 6.6523438 11.193359 L 6.8632812 11.484375 L 6.9296875 11.595703 L 6.9648438 11.720703 L 6.96875 11.847656 L 6.9375 11.974609 L 6.875 12.087891 L 6.7871094 12.181641 L 6.6777344 12.25 L 6.5527344 12.287109 L 5.6953125 12.449219 L 4.84375 12.646484 L 4.0019531 12.878906 L 3.1699219 13.146484 L 2.3515625 13.445312 L 2.0351562 13.591797 L 1.7382812 13.771484 L 1.4648438 13.986328 L 1.2167969 14.230469 L 0.99804688 14.5 L 0.81445312 14.794922 L 0.6640625 15.109375 L 0.46484375 15.652344 L 0.296875 16.208984 L 0.16796875 16.775391 L 0.07421875 17.345703 L 0.01953125 17.923828 L 0 18.503906 L 0.01953125 18.644531 L 0.080078125 18.775391 L 0.171875 18.882812 L 0.29296875 18.958984 L 0.42773438 19 L 0.57226562 19 L 0.70703125 18.958984 L 0.828125 18.882812 L 0.91992188 18.775391 L 0.98046875 18.644531 L 1 18.503906 L 1.0175781 17.988281 L 1.0664062 17.476562 L 1.1484375 16.964844 L 1.2636719 16.464844 L 1.4121094 15.96875 L 1.5898438 15.486328 L 1.7089844 15.242188 L 1.859375 15.017578 L 2.0390625 14.816406 L 2.2441406 14.640625 L 2.4707031 14.492188 L 2.7148438 14.378906 L 3.4960938 14.089844 L 4.2871094 13.835938 L 5.0878906 13.615234 L 5.9003906 13.427734 L 6.7167969 13.273438 L 6.9472656 13.216797 L 7.1699219 13.123047 L 7.3710938 12.996094 L 7.5507812 12.835938 L 7.7011719 12.652344 L 7.8222656 12.443359 L 7.9101562 12.220703 L 7.9570312 11.986328 L 7.9707031 11.748047 L 7.9433594 11.509766 L 7.8789062 11.279297 L 7.7792969 11.0625 L 7.6484375 10.865234 L 7.4902344 10.644531 L 7.3535156 10.416016 L 7.0742188 9.8554688 L 6.8261719 9.2792969 L 6.609375 8.6933594 L 6.4238281 8.09375 L 6.2734375 7.4863281 L 6.1542969 6.8710938 L 6.0683594 6.2519531 L 6.015625 5.6289062 L 6 5.0019531 L 6.0195312 4.609375 L 6.078125 4.2207031 L 6.171875 3.8398438 L 6.3046875 3.4707031 L 6.4726562 3.1152344 L 6.6738281 2.7792969 L 6.9082031 2.4628906 L 7.1699219 2.171875 L 7.4628906 1.9082031 L 7.7773438 1.6738281 L 8.1152344 1.4746094 L 8.4707031 1.3046875 L 8.8398438 1.1738281 L 9.21875 1.078125 L 9.6074219 1.0195312 L 10 1 L 10.392578 1.0195312 L 10.78125 1.078125 L 11.160156 1.1738281 L 11.529297 1.3046875 L 11.886719 1.4746094 L 12.222656 1.6738281 L 12.537109 1.9082031 L 12.830078 2.171875 L 13.091797 2.4628906 L 13.326172 2.7792969 L 13.527344 3.1152344 L 13.695312 3.4707031 L 13.828125 3.8398438 L 13.923828 4.2207031 L 13.982422 4.609375 L 14 5.0019531 L 13.984375 5.6289062 L 13.931641 6.2519531 L 13.845703 6.8710938 L 13.728516 7.4863281 L 13.576172 8.09375 L 13.390625 8.6933594 L 13.173828 9.2792969 L 12.925781 9.8554688 L 12.646484 10.416016 L 12.509766 10.644531 L 12.351562 10.865234 L 12.220703 11.0625 L 12.121094 11.279297 L 12.056641 11.509766 L 12.029297 11.748047 L 12.042969 11.986328 L 12.091797 12.220703 L 12.177734 12.443359 L 12.298828 12.652344 L 12.451172 12.835938 L 12.628906 12.996094 L 12.830078 13.123047 L 13.052734 13.216797 L 13.283203 13.273438 L 14.099609 13.427734 L 14.912109 13.615234 L 15.712891 13.835938 L 16.505859 14.089844 L 17.285156 14.378906 L 17.529297 14.492188 L 17.755859 14.640625 L 17.960938 14.816406 L 18.140625 15.017578 L 18.291016 15.242188 L 18.410156 15.486328 L 18.587891 15.96875 L 18.736328 16.464844 L 18.851562 16.964844 L 18.933594 17.476562 L 18.982422 17.988281 L 19 18.503906 L 19.019531 18.644531 L 19.080078 18.775391 L 19.171875 18.882812 L 19.292969 18.958984 L 19.427734 19 L 19.572266 19 L 19.708984 18.958984 L 19.828125 18.882812 L 19.919922 18.775391 L 19.980469 18.644531 L 20 18.503906 L 19.980469 17.923828 L 19.925781 17.345703 L 19.832031 16.773438 L 19.703125 16.208984 L 19.535156 15.652344 L 19.335938 15.109375 L 19.185547 14.794922 L 19.001953 14.5 L 18.783203 14.230469 L 18.535156 13.986328 L 18.263672 13.771484 L 17.966797 13.591797 L 17.648438 13.445312 L 16.830078 13.146484 L 15.998047 12.878906 L 15.15625 12.646484 L 14.306641 12.449219 L 13.447266 12.287109 L 13.322266 12.25 L 13.212891 12.181641 L 13.125 12.087891 L 13.0625 11.974609 L 13.033203 11.847656 L 13.035156 11.720703 L 13.070312 11.595703 L 13.136719 11.484375 L 13.347656 11.193359 L 13.529297 10.884766 L 13.833984 10.275391 L 14.103516 9.6523438 L 14.339844 9.0136719 L 14.541016 8.3613281 L 14.705078 7.703125 L 14.833984 7.0351562 L 14.925781 6.359375 L 14.982422 5.6816406 L 15 5.0019531 L 14.982422 4.5664062 L 14.923828 4.1328125 L 14.830078 3.7070312 L 14.697266 3.2910156 L 14.53125 2.8886719 L 14.330078 2.5 L 14.097656 2.1328125 L 13.830078 1.7871094 L 13.537109 1.4648438 L 13.212891 1.171875 L 12.869141 0.90625 L 12.5 0.66992188 L 12.113281 0.46875 L 11.710938 0.30273438 L 11.294922 0.16992188 L 10.867188 0.076171875 L 10.435547 0.01953125 L 10 0 z " />
                    </g>
                </svg>
            );
        case "Call":
            return (
                <svg width="30px" height="30px" viewBox="0 0 64 64" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" role="img" className="iconify iconify--emojione-monotone" preserveAspectRatio="xMidYMid meet"><path d="M38.813 21.847c-.865-1.728-1.166-4.023 1.662-5.798c4.68-2.936 7.04-7.792 5.373-11.058C44.882 3.091 43.231 2 41.321 2c-1.942 0-3.881 1.113-5.459 3.139c-2.761 3.542-5.649 6.045-8.442 8.468c-2.795 2.424-5.435 4.712-7.368 7.648c-2.118 3.217-5.033 8.382-6.43 12.123c-2.268 6.09 2.096 13.284 6.098 15.562l.042.024l.045.021c3.488 1.689 7.428 3.63 8.098 4.015c2.303 2.034 8.118 7.588 8.176 7.645l.029.027l.031.026C37.136 61.55 38.3 62 39.509 62c2.09 0 4.077-1.389 4.834-3.377c.726-1.908.173-3.891-1.479-5.304l-4.229-3.785a4.207 4.207 0 0 0 2.16-1.648c.518-.769.791-1.7.805-2.662l.391-.036c2.379-.233 4.064-1.868 4.443-4.144c1.584-.178 2.881-.99 3.671-2.299c.577-.834.878-1.825.894-2.946c.047-3.298-1.307-7.856-12.186-13.952m10.725 13.931c-.014.898-.28 1.767-.666 2.195c-.365.406-1.166.753-2.033.838l-5.749.536c-1.196.124-1.884-.158-2.737-.802l-4.449-3.629c-1.639-1.323-1.579-3.231-.682-4.692a4.613 4.613 0 0 0-1.069 1.764c-2.746 1.34-6.093 1.338-8.905-.32a8.703 8.703 0 0 1-.939-.646a7.203 7.203 0 0 0 2.402 2.311c2.304 1.357 5.06 1.313 7.278.145c.086.807.475 1.596 1.279 2.245l4.45 3.628c.852.645 1.541.927 2.738.803l4.588-.421a4.233 4.233 0 0 1-.524 2.345c-.507.464-1.185.787-2.042.872l-5.75.536c-1.196.123-1.885-.158-2.736-.802l-4.452-3.631c-1.639-1.322-1.577-3.23-.682-4.692c-1.434 1.489-1.919 3.912.047 5.498l4.45 3.628c.854.646 1.533 1.163 2.74 1.085l3.895-.255c.309.698.186 1.715-.311 2.585c-.436.396-1.014.673-1.748.745l-4.918.129c-1.03.027-1.954-.381-2.684-.933l-3.477-2.531c-1.402-1.133-1.35-2.767-.584-4.019c-1.227 1.275-1.859 3.429-.176 4.788l3.777 2.822c.729.552 1.563.87 2.593.891l3.248.065l6.195 5.545c.064.055.111.113.17.17c2.763 2.889-1.637 7.391-4.85 4.118c-2.531-2.574-4.978-5.28-9.846-8.262c-2.805-1.718-5.743-3.333-5.743-3.333c-3.485-1.983-7.271-7.917-5.299-13.208c1.331-3.575 4.026-8.424 6.14-11.635c3.646-5.54 9.977-9.064 15.675-16.374c.813-1.042 2.422-2.705 4.255-2.253c.883.218 1.607.97 2.136 2.009c1.29 2.526-.771 6.641-4.853 9.201c-3.773 2.366-3.273 5.748-1.971 8.052c11.016 6.087 11.854 10.385 11.819 12.889" fill="#000000"></path><path d="M41.802 31.448s-3.883-3.945-7.363-2.133c3.684-.813 7.322 4.168 7.322 4.168s1.383-1.489 3.586-1.318c-1.617-.852-3.545-.717-3.545-.717" fill="#000000"></path></svg>
            );
        case "Question": {
            return (
                <svg width="23px" height="23px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <circle cx="12" cy="12" r="8" stroke="#1C274C" strokeWidth="1.5" />
                    <path d="M10.125 8.875C10.125 7.83947 10.9645 7 12 7C13.0355 7 13.875 7.83947 13.875 8.875C13.875 9.56245 13.505 10.1635 12.9534 10.4899C12.478 10.7711 12 11.1977 12 11.75V13" stroke="#1C274C" strokeWidth="1.5" strokeLinecap="round" />
                    <circle cx="12" cy="16" r="1" fill="#1C274C" />
                </svg>
            );
        }
        case "TiltedTowers":
            return (
                <img src={require('./verzakking.png')} alt="tilted house" />
            )
        case "CampusGouda":
            return (
                <img className="w-20 h-20" src={require('./CampusGouda.png')} alt="tilted house" />
            )
        case "Bam":
            return (
                <img className="w-20 h-20" src={require('./Bam.jpg')} alt="tilted house" />
            )
        case "Rijkswaterstaat":
            return (
                <img className="w-40 h-20" src={require('./RIjkswaterstaat-logo.png')} alt="tilted house" />
            )

    }
    return <></>;
}

export default Icons;
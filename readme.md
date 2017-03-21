![Status](https://sebcza.visualstudio.com/_apis/public/build/definitions/46ac18f7-a64a-49e8-a7b3-baae13fd7f42/2/badge)
# H.A.R.I.S

IoT Experimental Platform

Projekt Haris to sieć czujników i urządzeń oparta na protokole HTTP. Urządzenia i czujniki bazują na urządzeniach Arduino oraz modułach bezprzewodowy nRF24.

### Słownik pojęć:

- Cube/Kostka — urządzenie lub czujnik 
- Cube Server — Aplikacja, która zarządza wiadomościami przesyłanymi w sieci. Tworzy usługę sieciową WebApi za pomocą której użytkownik może zarządzać urządzeniami. 
- Endpoint Cube — urządzenie podłączone do PC lub Raspberry PI, dzięki niemu HarisServer komunikuje się z Kostkami przez moduł bezprzewodowy nRF24 
- nRF24 - moduł bezprzewodowy, dzięki któremu możliwa jest bezprzewodowa komunikacja między urządzeniami. Pracuję on na częstotliwości 2.4 GHz tak jak WiFi oraz Bluetooth jednak wykorzystuje on najprostszą możliwą komunikację, co daje nam bardzo niskie zużycie energii i jest bardzo ważne w przypadku sensorów zasilanych bateriami. 

### Cube Server funkcje:

- Każda wiadomość wysyłana z kostki jest przesyłana do serwera. Kostki aktualnie nie mogą komunikować się bezpośrednio między sobą. 
- Cube server posiada reguły interakcji pomiędzy kostkami. Np. Decyduje, że wartość, która przyszła z Kostki Termometra zostanie wysłana na Kostkę Wyświetlacza oraz jeżeli wartość jest wyższa niż 30 stopni, to wyłączy grzałkę w akwarium, 
- Loguje wszystkie wiadomości, które odberał 
- Generuje adresy kostek — pięcio znakowy ciąg składający się z małych, dużych liter oraz cyfr. 
- Dekoduje wiadomości i decyduje jaki obiekt powołać do życia do obsługi danej kostki. 
- Obsługuje różne interfejsy komunikacji np. w wersji 1.0 HTTP, docelowo również HTTPS oraz WebSocket 

### Przykładowe kostki, na którymi pracujemy:

- Kostka wyświetlacz — kostka ze wbudowanym wyświetlaczem alfanumerycznym. Bezprzewodowo możemy przesyłać na wyświetlacz tekst. Np. CubeServer Posiada moduł, za pomocą którego sprawdzamy, czy na skrzynce mailowej są nowe wiadomości, jeżeli są, to wysyłamy stosowną informację na wyświetlacz. 
- Kostka Termometr — Kostka z termometrem zasilana bateriami. Dzięki optymalizacji Arduino baterie 2x AAA powinny zasilać urządzenie ok. 1 rok. Wysyła co pewien czas informację do CubeServera z aktualną wartością. Czas jest konfigurowalny. Użytkownik może zapisać wartość w bazie lub/i przesłać na Kostkę Wyświetlacza 
- Kostka Włącznik 230 V - Kostka, za pomocą której możemy sterować urządzeniami 230 V. Zaprojektowany w celu sterownia oświetleniem 

## Getting Started

### Endpoint Cube
It is Arduino based device. Haris Server can communicate with other wireless devices or sensors via Endpoint Cube, which is connected to PC via USB.

#### Components:
 - Arduino 
 - nRf24 module 
 - wires 
 
#### Instructions:
Connect the nrf24 to Arduino in this way:
![Images of Connection](https://microcontrollerelectronics.com/wp-content/uploads/2015/10/nRF24L01_Wiring.png)


### Installation Haris Server

Jak uruchomić i skonfigurować aplikację



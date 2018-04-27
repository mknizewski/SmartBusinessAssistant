using SBA.DAL.Context.InferenceDb.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Infrastructure
{
    public class SbaInferenceInitializer : CreateDatabaseIfNotExists<SbaInferenceContext>
    {
        public override void InitializeDatabase(SbaInferenceContext context)
        {
            if (!context.FaqAnswers.Any())
            {
                var answer1 = new FaqAnswers
                {
                    Answer = @"<div>
                    <p>
                        Specjalizujemy się w .NET. Nasze projekty są zgodne z wzorcem MVC (Model-Widok-Kontroler).
				        Naszym priorytetem jest bezpieczeństwo naszych aplikacji, aby nikt z naszych zleceniodawców nie stał
				        się ofiarą ataku hakerskiego. Design naszych programów jest nadzorowany przez speców w dziedzinie grafiki.
                    <p>
                    <p>
                        Nasze projekty realizujemy w oparciu o <strong>Microsoft .NET</strong>. Korzystamy z całego kalibry Microsoftu.
                    </p>
                    </div>"
                };

                var answer2 = new FaqAnswers
                {
                    Answer = @"<h3>WebCoding S.A.</h3>
						Białystok, Dojlidy <br>
						NIP: 984-00-99-777<br>
						<br>
						<abbr title=/""Telefon/"">T:</abbr>
                            48 777 999 777 < br >
                        < abbr title = /""E-mail/"" > E:</ abbr >
                        < a href = /""mailto:#/"" > biuro@webcodingsa.pl </ a > "
                };

                var answer3 = new FaqAnswers
                {
                    Answer = @"<div>
					<p>
					  W tej branży ciężko jest określić stałą kwotę za usługę. Każda aplikacja jest indywidualna i w taki sposób też jest wyceniana. Na końcową
					  cenę wpływ ma wiele czynników, takich jak:
					</p>
					<ul>
					  <li>Złożoność projektu</li>
					  <li>Układ oraz projekt graficzny</li>
					  <li>Wykorzystywane technologie</li>
					</ul>
					<hr>
					<p>
					  Wykonujemy na zlecenie:
					</p>
					<ul>
					  <li>Strony internetowe</li>
					  <li>Sklepy internetowe</li>
					  <li>Aplikacje mobilne</li>
					  <li>Oprogramowanie specjalistyczne</li>
					  <li>Oprogramowanie dedykowane</li>
					</ul>
					<p>Za godzinę usługi programistycznej zapłacisz około 80zł.</p>
				  </div>"
                };

                var answer4 = new FaqAnswers
                {
                    Answer = @" <div>
						<ul>
						  <li>
							<strong>Przewodnik po .NET</strong>
							<p>
								To aplikacja służąca do nauki programowania w języku C#. Zrealizowana dla osób chcących nauczyć się programowania. Autorskie 
								filmiki pokazują krok po kroku jak wygląda programowanie w C#. Opisy są bardzo szczegółowe i rzetelne, dzięki czemu użytkownik
								w szybki sposób zrozumie przekaz filmu.
							</p>
							<p>
							  Projekt został zrealizowany w ramach zajęć akademickich. Został przekazany dla UWB jako aplikacja naukowa dla studentów.
							</p>
						  </li>
						  <li>
							  <strong>TrainON</strong>
							  <p>
								  Aplikacja do zarządzania treningami zrealizowana w Android Studio. Program na urządzenia mobilne, dzięki któremu możemy 
								  planować swoje treningi oraz zapisywać osiągnięcia. Aplikacja posiadała również przypomnienia o dniu treningowym, jak również
								  motywowała do działania poprzez znane cytaty.
							  </p>
						  </li>
						  <li>
							  <strong>MineRed</strong>
							  <p>
								  Aplikacja służąca do zarządzania projektami. Dzięki niej grupa osób pracująca nad jednym zadaniem może w prosty sposób kontrolować
								  wzajemną pracę. Kierownik może sprawdzać to, jak przebiegają pracę i zlecać kolejne zadania dla swoich pracowników. 
							  </p>
						  </li>
						</ul>
					</div>"
                };

                var answer5 = new FaqAnswers
                {
                    Answer = @"<div>
                    <p>
                        Wraz z kolegami podczas studiów postanowiliśmy założyć firmę i tak też sie stało. 
						Zrealizowaliśmy już wiele dużych projektów, które poprawiły zakres naszych umiejętności, a co najważniejsze dodały nam
						jakże cennego doświadczenia w branży. 
                    </p>
                    <p>
                        Istniejemy na rynku już kilka dobrych lat.
                    </p>
                    </div>"
                };       

                var answer6 = new FaqAnswers
                {
                    Answer = @" <div>
					<p>W naszej firmie mamy do czynienia z 3 rodzajami rekrutacji.</p>
					<ol>
						<li><strong>Wewnętrzna</strong> - to wybór pracownika na nowe stanowisko spośród osób pracujących w naszej firmie.</li>
						<li><strong>Zewnętrzna</strong> - jest to wybranie kandydatów spośród osób spoza naszej firmy.</li>
						<li><strong>Mieszana</strong> - tutaj udział mogą wziąć zarówno osoby z firmy, jak i nowi kandydaci.</li>
					</ol>
					<hr>
					<p>
						Proces rekrutacji odbywa się w następujący sposób.
					</p>
					<ul>
						<li>
						<strong>1 etap</strong>
						<p>
							Kiedy nasza firma zadecyduje, że potrzebuje nowego pracownika, wówczas stara się ustalić zakres obowiązków
							dotyczących stanowiska. Po czym zamieszcza ogłoszenie z wymaganiami. 
						</p>
						</li>
						<li>
							<strong>2 etap</strong>
							<p>
							Zbieramy wszelkie CV, które zostają przesyłane na nasz adres e-mailowy. Zapoznajemy się z dokumentami i dokonujemy
							wstępnej selekcji.
							</p>
						</li>
						<li>
							<strong>3 etap</strong>
							<p>
							Proponujemy wykonanie jakiegoś prostego projektu, który będzie wykonany przez kandydata zdalnie. Kandydaci mogą również
							zostać poproszeni o przesłanie swoich projektów zawodowych, w celu zweryfikowania dodatkowych umiejętności.
							</p>
						</li>
						<li>
							<strong>4 etap</strong>
							<p>
							Jeśli zajdzie taka potrzeba, że po wykonaniu projektów dalej nie będzie tego jednego kandydata, który wybije się 
							poza innych, wówczas ma miejsce rozmowa telefoniczna z kandydatem. Osoby walczące o stanowisko mogą również zostać
							zaproszeni na rozmowę w siedzibie firmy.
							</p>
						</li>
						<li>
							<strong>5 etap</strong>
							<p>
							Tutaj zostaje wyłoniony ostateczny kandydat. Dochodzi do zatrudnienia i podpisania umowy.
							</p>
						</li>
					</ul>
				</div>"
                };

                var answer7 = new FaqAnswers
                {
                    Answer = @"<p>
					<strong>CV</strong> możesz złożyć na 3 sposoby:
					<ul>
					  <li>
						<strong>E-mail</strong>
						<p>Możesz przesłać nam CV w formie elektronicznej na nasz adres e-mail.</p>
					  </li>
					  <li>
						<strong>OLX</strong>
						<p>Nasze ogłoszenia o pracę umieszczamy w aplikacji OLX. Możesz zatem przesłać nam swoje CV bezpośrednio w niniejszej aplikacji</p>
					  </li>
					  <li>
						<strong>Osobiste złożenie w siedzibie firmy</strong>
						<p>CV możesz również wydrukować i przynieść do naszej siedziby.</p>
					  </li>
					</ul>
				  </p>"
                };

                var answer8 = new FaqAnswers
                {
                    Answer = @"<p>Aktualnie <strong>nie poszukujemy</strong> pracowników.</p>"
                };

                var answer9 = new FaqAnswers
                {
                    Answer = @"Klient w ciągu 14 dni od uzyskania systemu będzie musiał wpłacić odpowiednie należności 
                                za wytworzenie oprogramowania oraz dalszą konserwację wytworzonego systemu."
                };

                var answer10 = new FaqAnswers
                {
                    Answer = @"<div>
                        <p>Polityka poufności danych w naszej firmie: <p>
                        <ul>
                            <li>
                                Poufność danych klientów jest jedną z priorytetowych kwestii dla naszej firmy.
                            </li>
                            <li>
                                Nie kopiujemy dla własnych celów danych klienta i unikamy zapisywania danych na nośnikach.
                            </li>
                            <li>
                                Mamy ograniczony dostęp do szczegółowych danych klienta. Korzystamy z nich tych w ramach 
                                współpracy z tym klientem.
                            </li>
                        </ul>"
                };

                var answer11 = new FaqAnswers
                {
                    Answer = @"<div>
                        <p>Aby rozpocząć z nami współpracę należy wykonać jedną z trzech czynności: <p>
                        <ol>
                            <li>
                                Skontaktować się z naszą firmą telefonicznie, aby umówić się na spotkanie u klienta.
                            </li>
                            <li>
                                Wysłać zapytanie do nas drogą mailową odnośnie prośby na zaprojektowanie nowego systemu.
                            </li>
                            <li>
                                Przyjechać do nas osobiście, aby omówić zagadnienie wytworzenia nowego potencjalnego projektu.
                            </li>
                        </ol>"
                };

                var answer12 = new FaqAnswers
                {
                    Answer = @"<div>
                        <p>
                            Podczas podpisywania umowy, klient zobowiązuje się:
                        </p>
                       <ul>
                            <li>
                                Nie zmieniać firmy realizującej w późniejszym etapie projekt.
                            </li>
                            <li>
                                Uiszczać comiesięczną opłatę z konserwację wytworzonego systemu. 
                            </li>
                        </ul>
                        <p>
                            Okres umowy z firmą jest dożywodni.
                        </p>
                        </div>"
                };

                var answer13 = new FaqAnswers
                {
                    Answer = @"<div>
                        <p>
                            Firma zamierza w przyszłości rozszerzyć swoją działalność o:
                        </p>
                        <ul>
                            <li>
                                Wytwarzanie aplikacji mobilnych w Xamarin'nie oraz Apache Cordova.
                            </li>
                            <li>
                                Wspomaganie tworzenia i rozwoju aplikacji internetowych na pojedyneczej stronie
                                z wykorzystaniem frameworka AngularJS.
                            </li>
                        </ul>
                        </div>"
                };

                var answer14 = new FaqAnswers
                {
                    Answer = @"<div>
                        <p>
                            Realizacja projektu wygląda w następujący sposób:         
                        </p>
                        <ul>
                            <li>
                                <strong>Krok 1</strong>
                                <p>
                                Do danego klienta będzie początkowo przyjeżdżać analityk.
                                </p>
                            </li>
                            <li>
                                <strong>Krok 2</strong>
                                <p>
                                Potem na podstawie wytworzonej dokumentacji przez analityka, 
                                programiści będą wytwarzać oprogramowanie.
                                </p>
                            </li>
                            <li>
                                <strong>Krok 3</strong>
                                <p>
                                Po wytworzeniu oprogramownia będą przeprowadzone odpowiednie testy i na sam koniec system 
                                zostanie zaprezentowany klientowi w formie prezentacji w siedzibie firmy lub w siedzibie klienta.
                                </p>
                            </li>
                            <li>
                                <strong>Krok 4</strong>
                                <p>
                                Potem z zależności od poświęconych godzin nad systemem zostanie wyceniony system i o jego 
                                cenie zostanie poinformowany klient. Przekazany zostanie numer konta bankowego firmy.
                                </p>
                            </li>
                        <ul>
                        </div>"
                };

                var answer15 = new FaqAnswers
                {
                    Answer = @"<div>
                        <p>
                            Aktualnie <strong>poszukujemy</strong> zainteresowanych studentów na bezpłatny staż do naszej firmy.
                        </p>
                        <p>
                            Zainteresowane osoby prosimy o kontakt drogą mailową lub telefoniczną.
                        </p>
                        </div>"
                };

                context.FaqAnswers.AddRange(new List<FaqAnswers>
                {
                    answer1,
                    answer2,
                    answer3,
                    answer4,
                    answer5,
                    answer6,
                    answer7,
                    answer8,
                    answer9,
                    answer10,
                    answer11,
                    answer12,
                    answer13,
                    answer14,
                    answer15
                });
                context.SaveChanges();

                var question1 = new FaqQuestions
                {
                    AnswerId = answer1.Id,
                    Question = "Czym się zajmujemy?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion1 = new FaqQuestions
                {
                    AnswerId = answer1.Id,
                    Question = "W czym programujemy?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion1 = new FaqQuestions
                {
                    AnswerId = answer1.Id,
                    Question = "W jakich technologiach pisane są aplikacje?",
                    InsertTime = DateTime.Now
                };

                var question2 = new FaqQuestions
                {
                    AnswerId = answer2.Id,
                    Question = "Jaka jest lokalizacja firmy?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion2 = new FaqQuestions
                {
                    AnswerId = answer2.Id,
                    Question = "Gdzie mieści się placówka firmy?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion2 = new FaqQuestions
                {
                    AnswerId = answer2.Id,
                    Question = "Gdzie jest wasza siedziba?",
                    InsertTime = DateTime.Now
                };

                var question3 = new FaqQuestions
                {
                    AnswerId = answer3.Id,
                    Question = "Jakie są ceny za konkretne usługi?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion3 = new FaqQuestions
                {
                    AnswerId = answer3.Id,
                    Question = "Ile muszę zapłacić za wykonanie danej usługi?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion3 = new FaqQuestions
                {
                    AnswerId = answer3.Id,
                    Question = "Jak się cenimy?",
                    InsertTime = DateTime.Now
                };

                var question4 = new FaqQuestions
                {
                    AnswerId = answer4.Id,
                    Question = "Największe zrealizowane projekty?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion4 = new FaqQuestions
                {
                    AnswerId = answer4.Id,
                    Question = "Jakie są dotychczasowe osiągnięcia firmy?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion4 = new FaqQuestions
                {
                    AnswerId = answer4.Id,
                    Question = "Jakie są nasze największe jak do tej pory projekty?",
                    InsertTime = DateTime.Now
                };

                var question5 = new FaqQuestions
                {
                    AnswerId = answer5.Id,
                    Question = "Ile lat istniejemy na rynku?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion5 = new FaqQuestions
                {
                    AnswerId = answer5.Id,
                    Question = "Jak długo nasza firma istnieje na rynku?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion5 = new FaqQuestions
                {
                    AnswerId = answer5.Id,
                    Question = "Jak długo udzielamy usług programistycznych?",
                    InsertTime = DateTime.Now
                };

                var question6 = new FaqQuestions
                {
                    AnswerId = answer6.Id,
                    Question = "Jak wygląda proces rekrutacyjny?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion6 = new FaqQuestions
                {
                    AnswerId = answer6.Id,
                    Question = "Jak przebiega rekrutacja?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion6 = new FaqQuestions
                {
                    AnswerId = answer6.Id,
                    Question = "Jakich pytań mogę się spodziewać podczas rozmowy?",
                    InsertTime = DateTime.Now
                };

                var question7 = new FaqQuestions
                {
                    AnswerId = answer7.Id,
                    Question = "W jaki sposób mogę złożyć CV?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion7 = new FaqQuestions
                {
                    AnswerId = answer7.Id,
                    Question = "Jak mogę aplikować o pracę w Państwa firmie?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion7 = new FaqQuestions
                {
                    AnswerId = answer7.Id,
                    Question = "Jak rozpocząć karierę informatyczną w naszej firmie?",
                    InsertTime = DateTime.Now
                };

                var question8 = new FaqQuestions
                {
                    AnswerId = answer8.Id,
                    Question = "Czy są aktualne oferty pracy?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion8 = new FaqQuestions
                {
                    AnswerId = answer8.Id,
                    Question = "Czy aktualnie przyjmujecie programistów do pracy?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion8 = new FaqQuestions
                {
                    AnswerId = answer8.Id,
                    Question = "Czy potrzebujecie nowych pracowników, dodatkowej pary rąk do pomocy?",
                    InsertTime = DateTime.Now
                };

                var question9 = new FaqQuestions
                {
                    AnswerId = answer9.Id,
                    Question = "Jak wyglądają rozliczenia?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion9 = new FaqQuestions
                {
                    AnswerId = answer9.Id,
                    Question = "Jak przebiega etap finalizacji projektu związanych z rozliczeniem projektu?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion9 = new FaqQuestions
                {
                    AnswerId = answer9.Id,
                    Question = "Koszta finalizacji projektu?",
                    InsertTime = DateTime.Now
                };

                var question10 = new FaqQuestions
                {
                    AnswerId = answer10.Id,
                    Question = "Gdzie znajdują sie dane klientów?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion10 = new FaqQuestions
                {
                    AnswerId = answer10.Id,
                    Question = "Jak wygląda zasada poufności danych?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion10 = new FaqQuestions
                {
                    AnswerId = answer10.Id,
                    Question = "Czy dane klientów są bezpieczne?",
                    InsertTime = DateTime.Now
                };

                var question11 = new FaqQuestions
                {
                    AnswerId = answer11.Id,
                    Question = "Jak rozpocząć z nami współpracę?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion11 = new FaqQuestions
                {
                    AnswerId = answer11.Id,
                    Question = "Chcę zlecić wykonanie projektu? Co zrobić?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion11 = new FaqQuestions
                {
                    AnswerId = answer11.Id,
                    Question = "Jak wygląda etap wstępny analizy potencjalnego projektu?",
                    InsertTime = DateTime.Now
                };

                var question12 = new FaqQuestions
                {
                    AnswerId = answer12.Id,
                    Question = "Na jaki okres zawierana jest umowa z klientem?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion12 = new FaqQuestions
                {
                    AnswerId = answer12.Id,
                    Question = "Projekt został zrealizowany. Jak wygląda konserwacja systemu?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion12 = new FaqQuestions
                {
                    AnswerId = answer12.Id,
                    Question = "Czy po wykonaniu usługi będę miał pełny dostęp do napisanego przez Państwa kodu programu?",
                    InsertTime = DateTime.Now
                };

                var question13 = new FaqQuestions
                {
                    AnswerId = answer13.Id,
                    Question = "Czy planujecie w przyszłości rozszerzyć swoją działalność?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion13 = new FaqQuestions
                {
                    AnswerId = answer13.Id,
                    Question = "Jaka jest koncepcja Państwa firmy na przyszłość?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion13 = new FaqQuestions
                {
                    AnswerId = answer13.Id,
                    Question = "Czy zamierzacie rozszerzyć listę usług o dodatkowe w przyszłości?",
                    InsertTime = DateTime.Now
                };

                var question14 = new FaqQuestions
                {
                    AnswerId = answer14.Id,
                    Question = "Jak wygląda proces realizacji projektu?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion14 = new FaqQuestions
                {
                    AnswerId = answer14.Id,
                    Question = "W jaki sposób projektujemy zlecony przez klienta system?",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion14 = new FaqQuestions
                {
                    AnswerId = answer14.Id,
                    Question = "Jakie podejmujemy kroki, aby napisać projekt?",
                    InsertTime = DateTime.Now
                };

                var question15 = new FaqQuestions
                {
                    AnswerId = answer15.Id,
                    Question = "Czy przyjmujecie ludzi na staż?",
                    InsertTime = DateTime.Now
                };

                var firstVarietyOfQuestion15 = new FaqQuestions
                {
                    AnswerId = answer15.Id,
                    Question = "Czy zatrudniacie ludzi na douczanie? ",
                    InsertTime = DateTime.Now
                };

                var secondVarietyOfQuestion15 = new FaqQuestions
                {
                    AnswerId = answer15.Id,
                    Question = "Czy organizujecie kursy, douczania potencjalnym pracownikom, studentom?",
                    InsertTime = DateTime.Now
                };

                context.FaqQuestions.AddRange(new List<FaqQuestions>
                {
                    question1, firstVarietyOfQuestion1, secondVarietyOfQuestion1,
                    question2, firstVarietyOfQuestion2, secondVarietyOfQuestion2,
                    question3, firstVarietyOfQuestion3, secondVarietyOfQuestion3,
                    question4, firstVarietyOfQuestion4, secondVarietyOfQuestion4,
                    question5, firstVarietyOfQuestion5, secondVarietyOfQuestion5,
                    question6, firstVarietyOfQuestion6, secondVarietyOfQuestion6,
                    question7, firstVarietyOfQuestion7, secondVarietyOfQuestion7,
                    question8, firstVarietyOfQuestion8, secondVarietyOfQuestion8,
                    question9, firstVarietyOfQuestion9, secondVarietyOfQuestion9,
                    question10, firstVarietyOfQuestion10, secondVarietyOfQuestion10,
                    question11, firstVarietyOfQuestion11, secondVarietyOfQuestion11,
                    question12, firstVarietyOfQuestion12, secondVarietyOfQuestion12,
                    question13, firstVarietyOfQuestion13, secondVarietyOfQuestion13,
                    question14, firstVarietyOfQuestion14, secondVarietyOfQuestion14,
                    question15, firstVarietyOfQuestion15, secondVarietyOfQuestion15,
                });
                context.SaveChanges();
            }
        }
    }
}
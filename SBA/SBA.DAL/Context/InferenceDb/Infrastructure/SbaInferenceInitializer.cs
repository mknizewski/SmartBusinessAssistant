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
                    Answer = @"Specjalizujemy się w .NET. Nasze projekty są zgodne z wzorcem MVC (Model-Widok-Kontroler).
						       Naszym priorytetem jest bezpieczeństwo naszych aplikacji, aby nikt z naszych zleceniodawców nie stał
						       się ofiarą ataku hakerskiego. Design naszych programów jest nadzorowany przez speców w dziedzinie grafiki."
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
					  cenę wpływ ma wiele czynnikó, takich jak:
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
                    Answer = @"Wraz z kolegami podczas studiów postanowiliśmy założyć firmę i tak też sie stało. 
						Zrealizowaliśmy już wiele dużych projektów, które poprawiły zakres naszych umiejętności, a co najważniejsze dodały nam
						jakże cennego doświadczenia w branży. "
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
                    Answer = @"Nasze projekty realizujemy w oparciu o <strong>Microsoft .NET</strong>. Korzystamy z całego kalibry Microsoftu."
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
                    answer9
                });
                context.SaveChanges();

                var question1 = new FaqQuestions
                {
                    AnswerId = answer1.Id,
                    Question = "Czym się zajmujemy",
                    InsertTime = DateTime.Now
                };

                var question2 = new FaqQuestions
                {
                    AnswerId = answer2.Id,
                    Question = "Jaka jest lokalizacja firmy",
                    InsertTime = DateTime.Now
                };

                var question3 = new FaqQuestions
                {
                    AnswerId = answer3.Id,
                    Question = "Jakie są ceny za konkretne usługi",
                    InsertTime = DateTime.Now
                };

                var question4 = new FaqQuestions
                {
                    AnswerId = answer4.Id,
                    Question = "Największe zrealizowane projekty",
                    InsertTime = DateTime.Now
                };

                var question5 = new FaqQuestions
                {
                    AnswerId = answer5.Id,
                    Question = "Ile lat istniejemy na rynku",
                    InsertTime = DateTime.Now
                };

                var question6 = new FaqQuestions
                {
                    AnswerId = answer6.Id,
                    Question = "Jak wygląda proces rekrutacyjny",
                    InsertTime = DateTime.Now
                };

                var question7 = new FaqQuestions
                {
                    AnswerId = answer7.Id,
                    Question = "W jaki sposób mogę złożyć CV",
                    InsertTime = DateTime.Now
                };

                var question8 = new FaqQuestions
                {
                    AnswerId = answer8.Id,
                    Question = "Czy są aktualne oferty pracy",
                    InsertTime = DateTime.Now
                };

                var question9 = new FaqQuestions
                {
                    AnswerId = answer9.Id,
                    Question = "W jakich technologiach pisane są aplikacje",
                    InsertTime = DateTime.Now
                };

                context.FaqQuestions.AddRange(new List<FaqQuestions>
                {
                    question1,
                    question2,
                    question3,
                    question4,
                    question5,
                    question6,
                    question7,
                    question8,
                    question9
                });
                context.SaveChanges();
            }
        }
    }
}
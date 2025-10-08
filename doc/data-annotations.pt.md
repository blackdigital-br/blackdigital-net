# Sistema de DataAnnotations do BlackDigital

## Introdução

O sistema de DataAnnotations do BlackDigital fornece uma coleção de atributos personalizados para validação de dados e controle de exibição de propriedades. Este sistema estende as funcionalidades padrão do .NET com validações específicas e controles de visibilidade condicionais.

## Conceitos Básicos

### Tipos de Atributos

O sistema é dividido em duas categorias principais:

1. **Atributos de Validação**: Verificam se os dados atendem a critérios específicos
2. **Atributos de Exibição**: Controlam quando e como as propriedades devem ser exibidas

### DataValidator

O `DataValidator` é o núcleo do sistema, fornecendo métodos estáticos para validação:

```csharp
public static class DataValidator
{
    // Validação de email usando regex
    public static bool IsEmail(string email)
    
    // Validação de telefone usando regex  
    public static bool IsPhone(string phone)
    
    // Validação de email ou telefone
    public static bool IsEmailOrPhone(string emailOrPhone)
    
    // Comparação de valores usando símbolos
    public static bool ValidateData(object? value, object? otherValue, Symbol symbol)
}
```

## Atributos de Validação

### 1. EmailAttribute

Valida se o valor é um endereço de email válido.

```csharp
public class Usuario
{
    [Email(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
}
```

**Exemplo de uso:**
```csharp
var usuario = new Usuario { Email = "teste@exemplo.com" }; // ✅ Válido
var usuario2 = new Usuario { Email = "email-inválido" };   // ❌ Inválido
```

### 2. MobileAttribute

Valida se o valor é um número de telefone válido.

```csharp
public class Contato
{
    [Mobile(ErrorMessage = "Número de telefone inválido")]
    public string Telefone { get; set; }
}
```

**Exemplo de uso:**
```csharp
var contato = new Contato { Telefone = "+5511999999999" }; // ✅ Válido
var contato2 = new Contato { Telefone = "123" };           // ❌ Inválido
```

### 3. EmailMobileAttribute

Valida se o valor é um email OU um telefone válido.

```csharp
public class Cadastro
{
    [EmailMobile(ErrorMessage = "Informe um email ou telefone válido")]
    public string Contato { get; set; }
}
```

**Exemplo de uso:**
```csharp
var cadastro1 = new Cadastro { Contato = "teste@exemplo.com" };  // ✅ Válido (email)
var cadastro2 = new Cadastro { Contato = "+5511999999999" };     // ✅ Válido (telefone)
var cadastro3 = new Cadastro { Contato = "inválido" };           // ❌ Inválido
```

### 4. RequiredIfAttribute<T>

Torna uma propriedade obrigatória baseada em uma função personalizada.

```csharp
public class Pedido
{
    public TipoPagamento TipoPagamento { get; set; }
    
    [RequiredIf<Pedido>(p => p.TipoPagamento == TipoPagamento.CartaoCredito, 
                        ErrorMessage = "Número do cartão é obrigatório")]
    public string NumeroCartao { get; set; }
}
```

**Exemplo avançado com validação customizada:**
```csharp
public class Funcionario
{
    public bool EhGerente { get; set; }
    public decimal Salario { get; set; }
    
    [RequiredIf<Funcionario>((funcionario, context) => 
    {
        if (funcionario.EhGerente && funcionario.Salario < 5000)
        {
            return new ValidationResult("Gerentes devem ter salário mínimo de R$ 5.000");
        }
        return ValidationResult.Success;
    })]
    public string CodigoGerencia { get; set; }
}
```

### 5. RequiredIfPropertyAttribute

Torna uma propriedade obrigatória baseada no valor de outra propriedade.

```csharp
public class Endereco
{
    public string Pais { get; set; }
    
    [RequiredIfProperty("Pais", "Brasil", ErrorMessage = "CEP é obrigatório para endereços no Brasil")]
    public string CEP { get; set; }
    
    [RequiredIfProperty("Pais", "Brasil", "Argentina", ErrorMessage = "Estado é obrigatório")]
    public string Estado { get; set; }
}
```

**Exemplo com inversão:**
```csharp
public class Produto
{
    public bool EhDigital { get; set; }
    
    [RequiredIfProperty("EhDigital", false, IsInverted = true, 
                        ErrorMessage = "Peso é obrigatório para produtos físicos")]
    public decimal? Peso { get; set; }
}
```

### 6. CompareValueAttribute

Compara o valor da propriedade com outra propriedade usando símbolos de comparação.

```csharp
public class RegistroUsuario
{
    public string Senha { get; set; }
    
    [CompareValue("Senha", Symbol.Equal, ErrorMessage = "Senhas não coincidem")]
    public string ConfirmarSenha { get; set; }
    
    public DateTime DataNascimento { get; set; }
    
    [CompareValue("DataNascimento", Symbol.LessThan, ErrorMessage = "Data de cadastro deve ser posterior ao nascimento")]
    public DateTime DataCadastro { get; set; }
}
```

## Símbolos de Comparação

O enum `Symbol` define os operadores de comparação disponíveis:

```csharp
public enum Symbol
{
    Equal,              // ==
    NotEqual,           // !=
    LessThan,           // <
    LessThanOrEqual,    // <=
    GreaterThan,        // >
    GreaterThanOrEqual  // >=
}
```

**Exemplos práticos:**
```csharp
public class Evento
{
    public DateTime DataInicio { get; set; }
    
    [CompareValue("DataInicio", Symbol.GreaterThanOrEqual, 
                  ErrorMessage = "Data de fim deve ser posterior ou igual ao início")]
    public DateTime DataFim { get; set; }
    
    public decimal PrecoMinimo { get; set; }
    
    [CompareValue("PrecoMinimo", Symbol.GreaterThan, 
                  ErrorMessage = "Preço máximo deve ser maior que o mínimo")]
    public decimal PrecoMaximo { get; set; }
}
```

## Atributos de Exibição

### 1. ShowAttribute (Classe Base)

Classe abstrata que define a base para controle de visibilidade.

```csharp
public abstract class ShowAttribute : Attribute
{
    public abstract bool Show(object value);
}
```

### 2. ShowIfAttribute<T>

Controla a visibilidade baseada em uma função personalizada.

```csharp
public class ConfiguracaoAvancada
{
    public NivelUsuario Nivel { get; set; }
    
    [ShowIf<ConfiguracaoAvancada>(config => config.Nivel == NivelUsuario.Administrador)]
    public string ConfiguracaoAdmin { get; set; }
    
    [ShowIf<ConfiguracaoAvancada>(config => config.Nivel >= NivelUsuario.Moderador)]
    public bool PermitirModeração { get; set; }
}
```

### 3. ShowIfPropertyAttribute

Controla a visibilidade baseada no valor de outra propriedade.

```csharp
public class FormularioContato
{
    public TipoContato Tipo { get; set; }
    
    [ShowIfProperty("Tipo", TipoContato.Empresa, TipoContato.Fornecedor)]
    public string CNPJ { get; set; }
    
    [ShowIfProperty("Tipo", TipoContato.PessoaFisica)]
    public string CPF { get; set; }
    
    [ShowIfProperty("Tipo", TipoContato.Empresa, IsInverted = true)]
    public string NomeCompleto { get; set; }
}
```

**Exemplo com múltiplos valores:**
```csharp
public class StatusPedido
{
    public HttpStatusCode Status { get; set; }
    
    [ShowIfProperty("Status", HttpStatusCode.OK, HttpStatusCode.Created, 
                    HttpStatusCode.Accepted, HttpStatusCode.NoContent)]
    public string MensagemSucesso { get; set; }
    
    [ShowIfProperty("Status", HttpStatusCode.OK, HttpStatusCode.Created, 
                    IsInverted = true)]
    public string DetalhesErro { get; set; }
}
```

### 4. NotShow

Sempre oculta a propriedade (útil para propriedades que nunca devem ser exibidas).

```csharp
public class Usuario
{
    public string Nome { get; set; }
    
    [NotShow]
    public string SenhaHash { get; set; }
    
    [NotShow]
    public string ChaveInterna { get; set; }
}
```

## Exemplos Complexos

### Formulário de Cadastro Completo

```csharp
public class CadastroCompleto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }
    
    [EmailMobile(ErrorMessage = "Informe um email ou telefone válido")]
    public string Contato { get; set; }
    
    public TipoPessoa TipoPessoa { get; set; }
    
    [RequiredIfProperty("TipoPessoa", TipoPessoa.Fisica, ErrorMessage = "CPF é obrigatório")]
    [ShowIfProperty("TipoPessoa", TipoPessoa.Fisica)]
    public string CPF { get; set; }
    
    [RequiredIfProperty("TipoPessoa", TipoPessoa.Juridica, ErrorMessage = "CNPJ é obrigatório")]
    [ShowIfProperty("TipoPessoa", TipoPessoa.Juridica)]
    public string CNPJ { get; set; }
    
    public bool TemEndereco { get; set; }
    
    [RequiredIfProperty("TemEndereco", true, ErrorMessage = "CEP é obrigatório")]
    [ShowIfProperty("TemEndereco", true)]
    public string CEP { get; set; }
    
    [ShowIfProperty("TemEndereco", true)]
    public string Endereco { get; set; }
    
    public DateTime DataNascimento { get; set; }
    
    [CompareValue("DataNascimento", Symbol.GreaterThan, 
                  ErrorMessage = "Data de cadastro deve ser posterior ao nascimento")]
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    [NotShow]
    public string TokenInterno { get; set; }
}
```

### Sistema de Configuração Condicional

```csharp
public class ConfiguracaoSistema
{
    public bool HabilitarNotificacoes { get; set; }
    
    [ShowIfProperty("HabilitarNotificacoes", true)]
    [RequiredIfProperty("HabilitarNotificacoes", true, ErrorMessage = "Email é obrigatório para notificações")]
    [Email(ErrorMessage = "Email inválido")]
    public string EmailNotificacao { get; set; }
    
    [ShowIfProperty("HabilitarNotificacoes", true)]
    public int IntervaloMinutos { get; set; }
    
    public TipoAutenticacao TipoAuth { get; set; }
    
    [ShowIfProperty("TipoAuth", TipoAutenticacao.LDAP, TipoAutenticacao.ActiveDirectory)]
    [RequiredIfProperty("TipoAuth", TipoAutenticacao.LDAP, TipoAutenticacao.ActiveDirectory)]
    public string ServidorAuth { get; set; }
    
    [ShowIfProperty("TipoAuth", TipoAutenticacao.OAuth)]
    [RequiredIfProperty("TipoAuth", TipoAutenticacao.OAuth)]
    public string ClientId { get; set; }
    
    [ShowIfProperty("TipoAuth", TipoAutenticacao.OAuth)]
    [NotShow] // Nunca exibir, mas validar se necessário
    public string ClientSecret { get; set; }
}
```

## Como Funciona Internamente

### Processo de Validação

1. **Atributos de Validação**: Herdam de `ValidationAttribute` e implementam `IsValid()`
2. **DataValidator**: Fornece métodos estáticos para validações específicas
3. **Reflection**: Usado para acessar propriedades relacionadas em tempo de execução
4. **ValidationContext**: Fornece contexto sobre o objeto sendo validado

### Processo de Exibição

1. **ShowAttribute**: Define o contrato base com método `Show()`
2. **Reflection**: Usado para acessar valores de propriedades relacionadas
3. **Avaliação Condicional**: Determina se a propriedade deve ser exibida

### Fluxo de Execução

```
Objeto → Validação → Atributos → DataValidator → Resultado
                  ↓
              Exibição → ShowAttribute → Avaliação → Visibilidade
```

## Vantagens do Sistema

1. **Flexibilidade**: Validações e exibições condicionais baseadas em lógica customizada
2. **Reutilização**: Atributos podem ser aplicados em múltiplas propriedades
3. **Separação de Responsabilidades**: Validação e exibição são tratadas separadamente
4. **Integração**: Funciona com o sistema de validação padrão do .NET
5. **Extensibilidade**: Fácil criação de novos atributos personalizados

## Boas Práticas

1. **Mensagens de Erro Claras**: Sempre forneça mensagens de erro descritivas
2. **Validação Consistente**: Use os mesmos padrões em todo o sistema
3. **Performance**: Evite lógica complexa em atributos que são executados frequentemente
4. **Testes**: Sempre teste as validações com casos válidos e inválidos
5. **Documentação**: Documente validações complexas para facilitar manutenção

## Exemplo de Teste

```csharp
[Fact]
public void ShowIfPropertyAttribute_DeveExibirQuandoStatusCorreto()
{
    var showAttribute = new ShowIfPropertyAttribute("HttpStatus", 
        HttpStatusCode.OK, HttpStatusCode.Created);
    
    var modelo = new SimpleModel { HttpStatus = HttpStatusCode.OK };
    
    Assert.True(showAttribute.Show(modelo));
}

[Fact]
public void EmailAttribute_DeveValidarEmailCorreto()
{
    var emailAttribute = new EmailAttribute();
    
    Assert.True(emailAttribute.IsValid("teste@exemplo.com"));
    Assert.False(emailAttribute.IsValid("email-inválido"));
}
```

Este sistema de DataAnnotations fornece uma base sólida para validação e controle de exibição em aplicações .NET, oferecendo flexibilidade e facilidade de uso para cenários complexos de validação condicional.